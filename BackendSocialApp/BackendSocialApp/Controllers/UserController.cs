﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BackendSocialApp.Requests;
using BackendSocialApp.Tools;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using BackendSocialApp.ViewModels;
using AutoMapper;
using BackendSocialApp.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BackendSocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationSettings _appSettings;
        private readonly IEmailHelper _emailHelper;
        private readonly IHostingEnvironment _environment;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public IConfiguration _configuration { get; }

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<ApplicationSettings> appSettings,
                IEmailHelper emailHelper, IHostingEnvironment environment, IConfiguration configuration, ILogger<UserController> logger,
                IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _emailHelper = emailHelper;
            _environment = environment;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            request.UserName = request.UserName != null ? request.UserName.Trim() : request.UserName;

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserName);
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                if(!user.EmailConfirmed)
                {
                    throw new BusinessException("EmailNotConfirmed", "Lütfen mailinize gönderilen onay linkine tıklayınız.");
                }

                var role = await _userManager.GetRolesAsync(user);

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.JWT_Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(Constants.ClaimUserId, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role[0])
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                var token = tokenHandler.WriteToken(securityToken);

                if (role[0] == Constants.RoleAdmin)
                {
                    //var adminUser = (AdminUser)user;
                    return Ok(new { UserId = user.Id.ToString(), user.UserName, Token = token, Role = role[0], user.FullName, user.IsTestUser, user.PicturePath });
                }
                else if (role[0] == "Falci")
                {
                    var fortuneTellerUser = (FortuneTellerUser)user;
                    return Ok(new { UserId = user.Id.ToString(), user.UserName, Token = token, Role = role[0], fortuneTellerUser.CoffeePointPrice, fortuneTellerUser.CoffeFortuneTellingCount, user.FullName, user.IsTestUser, user.PicturePath });
                }
                else
                {
                    var consumerUser = (ConsumerUser)user;
                    return Ok(new { UserId = user.Id.ToString(), user.UserName, Token = token, Role = role[0], consumerUser.Point, user.FullName, user.IsTestUser, user.PicturePath });
                }
            }
            else
            {
                throw new BusinessException("WrongUserNameOrPassword", "Kullanıcı adı ya da şifre yanlış.");
            }
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public async Task<ActionResult> RegisterNewUser(RegisterNewUserRequest request)
        {
            request.UserName = request.UserName != null ? request.UserName.Trim() : request.UserName;
            request.Email = request.Email != null ? request.Email.Trim() : request.Email;
            request.Password = request.Password.Trim();
            request.Password2 = request.Password2.Trim();

            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                throw new BusinessException("EmptyUserName", "Kullanıcı adı boş olamaz.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new BusinessException("EmptyEmail", "Email boş olamaz.");
            }

            if(!Validations.IsValidEmail(request.Email)) {
                throw new BusinessException("NotValidEmail", "Geçerli bir E-Mail adresi girin.");
            }

            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                throw new BusinessException("EmptyFullname", "İsim boş olamaz.");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new BusinessException("EmptyPassword", "Şifre boş olamaz.");
            }

            if (request.Password != request.Password2)
            {
                throw new BusinessException("PasswordsMustBeSame", "Şifreler eşit olmalı.");
            }

            if (request.Password.Length < 6)
            {
                throw new BusinessException("PasswordLengthError", "Şifre 6 karakterden az olamaz.");
            }

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new BusinessException("UserAlreadyExists", "Kullanıcı zaten mevcut.");
            }

            var userByMail = await _userManager.FindByEmailAsync(request.Email);

            if (userByMail != null)
            {
                throw new BusinessException("EmailAlreadyExists", "Email zaten mevcut.");
            }

            var user = new ConsumerUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                CreateDate = DateTime.UtcNow,
                Gender = request.Gender,
                RelationshipStatus = request.RelationshipStatus,
                Job = request.Job,
                PicturePath = "defaultProfilePicture.png"
            };

            if(request.BirthDate.HasValue)
            {
                user.BirthDate = new DateTime(request.BirthDate.Value.Year, request.BirthDate.Value.Month, request.BirthDate.Value.Day);
            }

            if (request.BirthTime.HasValue)
            {
                user.BirthTime = new DateTime(1, 1, 1, request.BirthTime.Value.Hour, request.BirthTime.Value.Minute, 0);
            }

            await _userManager.CreateAsync(user, request.Password);
            await _userManager.AddToRoleAsync(user, "Consumer");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var entryUrl = _configuration.GetValue<string>("EntryUrl");
            var emailConfirmationLink = entryUrl + "/#/newUserConfirmation?email=" + request.Email + "&token=" + WebUtility.UrlEncode(token);

            var userConfirmationMailTemplatePath = _environment.ContentRootPath + "\\Assets\\Templates\\UserConfirmationMail.html";
            var template = System.IO.File.ReadAllText(userConfirmationMailTemplatePath);
            template = template
                .Replace("[userName]", request.FullName)
                .Replace("[confirmationLink]", emailConfirmationLink);

            _emailHelper.Send(
                new EmailModel
                {
                    To = request.Email,
                    Subject = "Falcım - Kullanıcı Onayı",
                    IsBodyHtml = true,
                    Message = template
                }
            );

            return Ok();
        }

        [HttpPost]
        [Route("UpdateUserInfo")]
        public async Task<ActionResult> UpdateUserInfo(UpdateUserInfoRequest request)
        {
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                throw new BusinessException("EmptyFullname", "İsim boş olamaz.");
            }

            user.FullName = request.FullName;
            user.Gender = request.Gender;
            user.Description = request.Description;


            if (request.BirthDate.HasValue)
            {
                user.BirthDate = new DateTime(request.BirthDate.Value.Year, request.BirthDate.Value.Month, request.BirthDate.Value.Day);
            }

            var consumerUser = user as ConsumerUser;

            if(consumerUser != null)
            {
                consumerUser.RelationshipStatus = request.RelationshipStatus;
                consumerUser.Job = request.Job;

                if (request.BirthTime.HasValue)
                {
                    consumerUser.BirthTime = new DateTime(1, 1, 1, request.BirthTime.Value.Hour, request.BirthTime.Value.Minute, 0);
                }
            }

            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpPost]
        [Route("ConfirmNewUser")]
        public async Task<ActionResult> ConfirmNewUser(ConfirmNewUserRequest request)
        {
            request.Email = request.Email != null ? request.Email.Trim() : request.Email;

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new BusinessException("EmptyEmail", "Email boş olamaz.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (result.Succeeded)
            {
                return Ok();
            }
            var errors = "";
            foreach (var item in result.Errors)
            {
                errors = errors + item.Code + " - " + item.Description + ",";
            }

            _logger.LogError("ConfirmError: Email: " + request.Email + ", Errors: " + errors);

            throw new BusinessException("CanNotConfirm", "Onaylanamadı.");
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var user = await _userManager.FindByIdAsync(userId);
            request.NewPassword = request.NewPassword.Trim();
            request.NewPassword2 = request.NewPassword2.Trim();

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            if (false == await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
            {
                throw new BusinessException("CurrentPasswordIsWrong", "Mevcut şifre yanlış");
            }

            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                throw new BusinessException("EmptyPassword", "Şifre boş olamaz.");
            }

            if (request.NewPassword.Length < 6)
            {
                throw new BusinessException("PasswordLengthError", "Şifre 6 karakterden az olamaz.");
            }

            if (request.NewPassword != request.NewPassword2)
            {
                throw new BusinessException("PasswordsMustBeSame", "Şifreler eşit olmalı.");
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            var errors = "";
            foreach (var item in result.Errors)
            {
                errors = errors + item.Code + " - " + item.Description + ",";
            }

            _logger.LogError("ChangePassword: Email: " + user.Email + ", Errors: " + errors);

            throw new BusinessException("CanNotChange", "Şifre değiştirilemedi.");
        }

        [HttpPost]
        [Route("ForgatPassword")]
        public async Task<ActionResult> ForgatPassword(ForgatPasswordRequest request)
        {
            request.Email = request.Email != null ? request.Email.Trim() : request.Email;
            request.UserName = request.UserName != null ? request.UserName.Trim() : request.UserName;

            if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.UserName))
            {
                throw new BusinessException("EmptyEmail", "Email veya Kullanıcı isimi dolu olmalı.");
            }

            if (!string.IsNullOrWhiteSpace(request.Email) && !string.IsNullOrWhiteSpace(request.UserName))
            {
                throw new BusinessException("EmptyEmail", "Email veya Kullanıcı isimi dolu olmalı.");
            }

            ApplicationUser user = null;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                user = await _userManager.FindByEmailAsync(request.Email);
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UserName);
            }

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var entryUrl = _configuration.GetValue<string>("EntryUrl");
            var passwordResetLink = entryUrl + "/#/resetPassword?email=" + user.Email + "&token=" + WebUtility.UrlEncode(token);

            var templatePath = _environment.ContentRootPath + "\\Assets\\Templates\\PasswordResetMail.html";
            var template = System.IO.File.ReadAllText(templatePath);
            template = template
                .Replace("[userName]", user.FullName)
                .Replace("[passwordResetLink]", passwordResetLink);

            _emailHelper.Send(
                new EmailModel
                {
                    To = user.Email,
                    Subject = "Falcım - Şifre yenileme",
                    IsBodyHtml = true,
                    Message = template
                }
            );

            return Ok();
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest request)
        {
            request.Email = request.Email != null ? request.Email.Trim() : request.Email;

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new BusinessException("EmptyEmail", "Email boş olamaz.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            if (request.NewPassword.Length < 6)
            {
                throw new BusinessException("PasswordLengthError", "Şifre 6 karakterden az olamaz.");
            }

            if (request.NewPassword != request.NewPassword2)
            {
                throw new BusinessException("PasswordsMustBeSame", "Şifreler eşit olmalı.");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            var errors = "";
            foreach (var item in result.Errors)
            {
                errors = errors + item.Code + " - " + item.Description + ",";
            }

            _logger.LogError("ResetError: Email: " + request.Email + ", Errors: " + errors);

            throw new BusinessException("CanNotReset", "Şifre güncellenemedi.");
        }

        [HttpPost]
        [Authorize]
        [Route("UpdateProfilePhoto")]
        public async Task<ActionResult> UpdateProfilePhoto([FromForm]UpdateProfilePhotoRequest request)
        {
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            var folderPath = _environment.ContentRootPath + "\\Assets\\ProfilePictures\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (request.Photo.ContentType.Length > 30)
            {
                throw new Exception("Wrong Content-Type");
            }
            var fileName = Guid.NewGuid() + "." + request.Photo.ContentType.Replace('/', '.');
            var fileFullPath = folderPath + fileName;
            using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Create))
            {
                await request.Photo.CopyToAsync(fileStream);
            }

            user.PicturePath = fileName;
            await _userManager.UpdateAsync(user);

            return Ok(new { user.PicturePath });
        }

        [HttpGet]
        [Route("GetConsumerUserInfo")]
        public async Task<ActionResult<UserInfoViewModel>> GetConsumerUserInfo(string userName, Guid? consumerUserId)
        {
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var userRole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;

            if(string.IsNullOrEmpty(userName) && !consumerUserId.HasValue)
            {
                throw new BusinessException("MissingParameter", "Eksik parametre.");
            }

            ApplicationUser user = null;
            if(!string.IsNullOrEmpty(userName)) { 
                user = await _userManager.FindByNameAsync(userName);
            }
            else
            {
                user = await _userManager.FindByIdAsync(consumerUserId.Value.ToString());
            }

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            var consumerUser = user as ConsumerUser;
            if(consumerUser == null)
            {
                throw new BusinessException("NotConsumerUser", "Bu kayıt bir kullanıcı değil.");
            }

            if(user.Id.ToString() != userId && (userRole != Constants.RoleAdmin && userRole != Constants.RoleFalci))
            {
                throw new BusinessException("AuthError", "Bu kaydı görmeye yetkiniz bulunmuyor.");
            }

            var viewModel = _mapper.Map<ConsumerUser, UserInfoViewModel>(consumerUser);

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("BuyPoint")]
        public async Task<ActionResult> BuyPoint(BuyPointRequest request)
        {
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var consumerUser = user as ConsumerUser;
            if(consumerUser == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            var newPoint = await _userService.BuyPoint(consumerUser, request.TransactionJson, request.TransactionId, request.ProductId, request.PointType);

            return Ok(newPoint);
        }

        [HttpGet]
        [Route("GetPoints")]
        public ActionResult<List<PointViewModel>> GetPoints(PointType pointType)
        {
            var items = _userService.GetPoints(pointType).Result;
            var viewModel = _mapper.Map<List<Point>, List<PointViewModel>>(items);

            return Ok(viewModel);
        }
    }
}