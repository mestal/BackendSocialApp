using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Persistence.Repositories
{
    public class CoffeeFortuneTellingRepository : BaseRepository, ICoffeeFortuneTellingRepository
    {
        public CoffeeFortuneTellingRepository(AppDbContext context) : base(context) { }

        public async Task AddCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling)
        {
            await _context.CoffeeFortuneTellings.AddAsync(coffeeFortuneTelling);
        }

        public async Task AddCoffeeFortuneTellingPictureAsync(CoffeeFortuneTellingPicture coffeeFortuneTellingPicture)
        {
            await _context.CoffeeFortuneTellingPictures.AddAsync(coffeeFortuneTellingPicture);
        }
    }
}
