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

namespace BackendSocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationSettings _appSettings;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
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

            return Ok();
        }
    }
}