using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Domain.Services.Communication;
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
        public CoffeeFortuneTellingService(ICoffeeFortuneTellingRepository coffeeFortuneTellingRepository, IUnitOfWork unitOfWork)
        {
            _coffeeFortuneTellingRepository = coffeeFortuneTellingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCoffeeFortuneTellingResponse> CreateCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling, List<string> picturePaths)
        {
            try
            {
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
