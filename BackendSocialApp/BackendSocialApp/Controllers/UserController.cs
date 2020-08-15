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

        public IConfiguration _configuration { get; }

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<ApplicationSettings> appSettings,
                IEmailHelper emailHelper, IHostingEnvironment environment, IConfiguration configuration, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _emailHelper = emailHelper;
            _environment = environment;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

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
                    return Ok(new { UserId = user.Id.ToString(), request.UserName, Token = token, Role = role[0] });
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
            request.UserName = request.UserName.Trim();
            request.Email = request.Email.Trim();

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
            request.Email = request.Email.Trim();

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

            if(result.Succeeded) { 
                return Ok();
            }
            var errors = "";
            foreach(var item in result.Errors)
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
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            if (await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
            {
                throw new BusinessException("CurrentPasswordIsWrong", "Şifre yanlış");
            }

            if (request.NewPassword != request.NewPassword2)
            {
                throw new BusinessException("PasswordsMustBeSame", "Şifreler eşit olmalı.");
            }

            await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            return Ok();
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Email))  
            {
                throw new BusinessException("EmptyEmail", "Email boş olamaz.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = Url.Action("ResetPassword", "User", new { email = request.Email, token }, Request.Scheme);

            _emailHelper.Send(
                new EmailModel
                {
                    To = request.Email,
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

            await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            return Ok();
        }

        [HttpPost]
        [Route("UpdateProfilePhoto")]
        public async Task<ActionResult> UpdateProfilePhoto(UpdateProfilePhotoRequest request)
        {
            var userId = User.Claims.First(a => a.Type == "UserID").Value;
            var folderPath = _environment.ContentRootPath + "\\ProfilePhotos\\";
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
            user.PicturePath = fileFullPath;
            await _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}