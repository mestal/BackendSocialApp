using System;
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

        public IConfiguration _configuration { get; }

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<ApplicationSettings> appSettings,
                IEmailHelper emailHelper, IHostingEnvironment environment, IConfiguration configuration, ILogger<UserController> logger,
                IMapper mapper)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _emailHelper = emailHelper;
            _environment = environment;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
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
                    return Ok(new { UserId = user.Id.ToString(), user.UserName, Token = token, Role = role[0] });
                }
                else if (role[0] == "Falci")
                {
                    var fortuneTellerUser = (FortuneTellerUser)user;
                    return Ok(new { UserId = user.Id.ToString(), request.UserName, Token = token, Role = role[0], fortuneTellerUser.CoffeePointPrice, fortuneTellerUser.CoffeFortuneTellingCount });
                }
                else
                {
                    var consumerUser = (ConsumerUser)user;
                    return Ok(new { UserId = user.Id.ToString(), request.UserName, Token = token, Role = role[0], consumerUser.Point });
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

            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                throw new BusinessException("EmptyUserName", "Kullanıcı adı boş olamaz.");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new BusinessException("EmptyEmail", "Email boş olamaz.");
            }

            //TODO Regex for email.

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

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null)
            {
                throw new BusinessException("UserAlreadyExists", "Kullanıcı zaten mevcut.");
            }

            var userByMail = await _userManager.FindByEmailAsync(request.Email);

            if (userByMail != null)
            {
                throw new BusinessException("EmailAlreadyExists", "Email zaten mevcut.");
            }

            user = new ConsumerUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName
            };

            await _userManager.CreateAsync(user, request.Password);
            await _userManager.AddToRoleAsync(user, "Consumer");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var entryUrl = _configuration.GetValue<string>("EntryUrl");
            var emailConfirmationLink = entryUrl + "/#/newUserConfirmation?email=" + request.Email + "&token=" + WebUtility.UrlEncode(token);

            _emailHelper.Send(
                new EmailModel
                {
                    To = request.Email,
                    Subject = "Falcım - Kullanıcı Onayı",
                    IsBodyHtml = false,
                    Message = emailConfirmationLink
                }
            );

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

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            if (false == await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
            {
                throw new BusinessException("CurrentPasswordIsWrong", "Mevcut şifre yanlış");
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
            //TODO emaili onaylanmamış bir user forgat password yaparsa ne olur.
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

            _emailHelper.Send(
                new EmailModel
                {
                    To = user.Email,
                    Subject = "Falcım - Şifre yenileme",
                    IsBodyHtml = false,
                    Message = passwordResetLink
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
        [Route("UpdateProfilePhoto")]
        public async Task<ActionResult> UpdateProfilePhoto([FromForm]UpdateProfilePhotoRequest request)
        {
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
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
            using (FileStream fileStream = System.IO.File.Create(fileFullPath))
            {
                request.Photo.CopyTo(fileStream);
                fileStream.Flush();
            }

            var user = await _userManager.FindByIdAsync(userId);
            user.PicturePath = fileName;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [HttpGet]
        [Route("GetConsumerUserInfo")]
        public async Task<ActionResult<UserInfoViewModel>> GetConsumerUserInfo(string userName)
        {
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var userRole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;

            ApplicationUser user = null;
            user = await _userManager.FindByNameAsync(userName);

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
    }
}