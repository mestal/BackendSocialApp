using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Repositories
{
    public interface ICoffeeFortuneTellingRepository
    {
        Task AddCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling);

        Task AddCoffeeFortuneTellingPictureAsync(CoffeeFortuneTellingPicture coffeeFortuneTellingPicture);
    }
}
