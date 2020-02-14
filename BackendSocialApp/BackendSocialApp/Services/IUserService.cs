using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;

namespace BackendSocialApp.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(ApplicationUser user, string password, string roleName);
    }
}
