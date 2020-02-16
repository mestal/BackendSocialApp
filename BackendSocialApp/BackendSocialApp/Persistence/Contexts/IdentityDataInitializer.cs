using BackendSocialApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Persistence.Contexts
{
    public class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            var result = roleManager.RoleExistsAsync("Admin").Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            result = roleManager.RoleExistsAsync("Falci").Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Falci";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            result = roleManager.RoleExistsAsync("Consumer").Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Consumer";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser user = new AdminUser();
                user.UserName = "admin";
                user.Email = "admin@falci.com";
                user.FullName = "Admin User";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("falci1").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci1";
                user.Email = "falci1@falci.com";
                user.FullName = "Falcı User 1";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Falci").Wait();
                }
            }

            if (userManager.FindByNameAsync("falci2").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci2";
                user.Email = "falci1@falci.com";
                user.FullName = "Falcı User 2";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Falci").Wait();
                }
            }

            if (userManager.FindByNameAsync("falci3").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci3";
                user.Email = "falci1@falci.com";
                user.FullName = "Falcı User 3";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Falci").Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user1";
                user.Email = "user1@yahoo.com";
                user.FullName = "User 1";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Consumer").Wait();
                }
            }

            if (userManager.FindByNameAsync("user2").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user2";
                user.Email = "user2@yahoo.com";
                user.FullName = "User 2";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Consumer").Wait();
                }
            }

            if (userManager.FindByNameAsync("user3").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user3";
                user.Email = "user3@yahoo.com";
                user.FullName = "User 3";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Consumer").Wait();
                }
            }
        }
    }
}
