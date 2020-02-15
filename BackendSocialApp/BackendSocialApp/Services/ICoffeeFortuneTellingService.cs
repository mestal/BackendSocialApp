using System.Collections.Generic;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Services.Communication;
using Microsoft.AspNetCore.Http;

namespace BackendSocialApp.Services
{
    public interface ICoffeeFortuneTellingService
    {
        Task<CreateCoffeeFortuneTellingResponse> CreateCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling, List<string> picturePaths);
    }
}