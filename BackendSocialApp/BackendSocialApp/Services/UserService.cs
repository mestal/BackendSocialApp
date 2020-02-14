using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;

namespace BackendSocialApp.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateUserAsync(ApplicationUser user, string password, string roleName)
        {
            try
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, password);
                if(result.Succeeded)
                { 
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    throw new Exception("User cant created");//TODO
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
