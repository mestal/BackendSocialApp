using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Domain.Services.Communication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Services
{
    public class CoffeeFortuneTellingService : ICoffeeFortuneTellingService
    {

        public ICoffeeFortuneTellingRepository _coffeeFortuneTellingRepository;
        public IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CoffeeFortuneTellingService(ICoffeeFortuneTellingRepository coffeeFortuneTellingRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _coffeeFortuneTellingRepository = coffeeFortuneTellingRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<CreateCoffeeFortuneTellingResponse> CreateCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling, List<string> picturePaths)
        {

            if (!coffeeFortuneTelling.User.EmailConfirmed)
            {
                throw new Exception("EmailNotConfirmed");
            }

            if (coffeeFortuneTelling.FortuneTeller.Status == UserStatus.Deactive)
            {
                throw new Exception("FortuneTellerIsNotActive");
            }

            coffeeFortuneTelling.SubmitDateUtc = DateTime.UtcNow;

            if(coffeeFortuneTelling.User.Point < coffeeFortuneTelling.FortuneTeller.CoffeePoint)
            {
                throw new Exception("UserDoesntHaveEnoughPoint");
            }

            try
            {
                //TODO belki db den direk update etmek gerekebilir.
                coffeeFortuneTelling.User.Point -= coffeeFortuneTelling.FortuneTeller.CoffeePoint;
                await _userManager.UpdateAsync(coffeeFortuneTelling.User);

                await _coffeeFortuneTellingRepository.AddCoffeeFortuneTellingAsync(coffeeFortuneTelling);

                foreach(var item in picturePaths)
                {
                    await _coffeeFortuneTellingRepository.AddCoffeeFortuneTellingPictureAsync(new CoffeeFortuneTellingPicture
                    {
                        CoffeeFortuneTelling = coffeeFortuneTelling,
                        Path = item
                    });
                }

                await _unitOfWork.CompleteAsync();

                return new CreateCoffeeFortuneTellingResponse(coffeeFortuneTelling);
            }
            catch (Exception ex)
            {
                return new CreateCoffeeFortuneTellingResponse($"An error occurred when saving the employee: {ex.Message}");
            }
        }
    }
}
