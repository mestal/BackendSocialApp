using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BackendSocialApp.Requests;
using BackendSocialApp.Tools;

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

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<ApplicationSettings> appSettings,
                IEmailHelper emailHelper)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _emailHelper = emailHelper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("Role", role[0])
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { Token = token, Role = role[0] });

            }
            else
            {
                return BadRequest(new { message = "UserName or password is incorrect." });
            }
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public async Task<ActionResult> RegisterNewUser(RegisterNewUserRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null)
            {
                return BadRequest(new { message = "UserName already exists." });
            }

            if (request.Password != request.Password2)
            {
                return BadRequest(new { message = "Passwords not equal." });
            }

            var newUser = new ConsumerUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.Name + " " + request.Surname
            };

            await _userManager.CreateAsync(newUser, request.Password);
            await _userManager.AddToRoleAsync(newUser, "Consumer");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var emailConfirmationLink = Url.Action("EmailConfirmation", "User", new { email = request.Email, token }, Request.Scheme);

            _emailHelper.Send(
                new EmailModel
                {
                    To = request.Email,
                    Subject = "Falcı - Email Confirmation",
                    IsBodyHtml = false,
                    Message = emailConfirmationLink
                }
            );

            return Ok();
        }

        public async Task<ActionResult> EmailConfirmation(EmailConfirmationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { message = "Empty Email." });
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            await _userManager.ConfirmEmailAsync(user, request.Token);

            return Ok();
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            if (await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
            {
                return BadRequest(new { message = "Current password is wrong." });
            }

            if (request.NewPassword != request.NewPassword2)
            {
                return BadRequest(new { message = "Passwords not equal." });
            }

            await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            return Ok();
        }

        public async Task<ActionResult> ForgetPassword(ForgetPasswordRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.Email))  
            {
                return BadRequest(new { message = "Enter Email." });
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = Url.Action("ResetPassword", "User", new { email = request.Email, token }, Request.Scheme);

            _emailHelper.Send(
                new EmailModel
                {
                    To = request.Email,
                    Subject = "Falcı - Reset Password",
                    IsBodyHtml = false,
                    Message = passwordResetLink
                }
            );

            return Ok();
        }

        public async Task<ActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { message = "Empty Email." });
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            if (request.NewPassword != request.NewPassword2)
            {
                return BadRequest(new { message = "Passwords not equal." });
            }

            await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            return Ok();
        }
    }
}