using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Domain.Services.Communication;
using BackendSocialApp.Paging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendSocialApp.Services
{
    public class CoffeeFortuneTellingService : ICoffeeFortuneTellingService
    {

        public ICoffeeFortuneTellingRepository _coffeeFortuneTellingRepository;
        public IUnitOfWork _unitOfWork;

        public CoffeeFortuneTellingService(ICoffeeFortuneTellingRepository coffeeFortuneTellingRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _coffeeFortuneTellingRepository = coffeeFortuneTellingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCoffeeFortuneTellingResponse> CreateCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling, List<string> picturePaths)
        {

            if (!coffeeFortuneTelling.User.EmailConfirmed)
            {
                throw new BusinessException("EmailNotConfirmed", "E-mail onaylanmamış.");
            }

            if (coffeeFortuneTelling.FortuneTeller.Status == UserStatus.Deactive)
            {
                throw new BusinessException("FortuneTellerIsNotActive", "Falcı aktif değildir.");
            }

            coffeeFortuneTelling.SubmitDateUtc = DateTime.UtcNow;

            if (coffeeFortuneTelling.User.Point < coffeeFortuneTelling.FortuneTeller.CoffeePointPrice)
            {
                throw new BusinessException("UserDoesntHaveEnoughPoint", "Yeteri kadar puan bulunmamaktadır. Puan satın alınız.");
            }

            coffeeFortuneTelling.Status = CoffeeFortuneTellingStatus.SubmittedByUser;

            await _coffeeFortuneTellingRepository.AddCoffeeFortuneTellingAsync(coffeeFortuneTelling);

            foreach (var item in picturePaths)
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

        public async Task<SubmitFortuneTellingByFortuneTellerResponse> SubmitFortuneTellingByFortuneTeller(Guid fortuneTellerId, Guid coffeeFortuneTellingId, string comment)
        {
            var fortuneTelling = _coffeeFortuneTellingRepository.GetCoffeeFortuneTelling(coffeeFortuneTellingId);

            if(fortuneTelling == null)
            {
                throw new BusinessException("FortuneTellingNotFound", "Fal bulunamadı.");
            }

            if(fortuneTellerId != fortuneTelling.FortuneTeller.Id)
            {
                throw new BusinessException("FortuneTellingTellerDifferent", "Bu fal size gönderilmemiş.");
            }

            if(fortuneTelling.Status != CoffeeFortuneTellingStatus.SubmittedByUser)
            {
                throw new BusinessException("FortuneTellingStatusWrong", "Falın durumu uygun değil.");
            }

            fortuneTelling.FortuneTellerComment = comment;
            fortuneTelling.Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller;

            _coffeeFortuneTellingRepository.UpdateCoffeeFortuneTelling(fortuneTelling);
            await _unitOfWork.CompleteAsync();

            return new SubmitFortuneTellingByFortuneTellerResponse();
        }

        public async Task<IPagedList<CoffeeFortuneTelling>> GetUserItems(PageSearchArgs args, Guid userId)
        {
            var itemsPagedList = await _coffeeFortuneTellingRepository.GetUserItemsAsync(args, userId);

            //var categoryModels = ObjectMapper.Mapper.Map<List<CategoryModel>>(categoryPagedList.Items);

            var result = new PagedList<CoffeeFortuneTelling>(
                itemsPagedList.PageIndex,
                itemsPagedList.PageSize,
                itemsPagedList.TotalCount,
                itemsPagedList.TotalPages,
                itemsPagedList.Items);

            return result;
        }

        public async Task<IPagedList<CoffeeFortuneTelling>> GetFortuneTellerItems(PageSearchArgs args, Guid userId)
        {
            var itemsPagedList = await _coffeeFortuneTellingRepository.GetFortuneTellerItemsAsync(args, userId);

            //var categoryModels = ObjectMapper.Mapper.Map<List<CategoryModel>>(categoryPagedList.Items);

            var result = new PagedList<CoffeeFortuneTelling>(
                itemsPagedList.PageIndex,
                itemsPagedList.PageSize,
                itemsPagedList.TotalCount,
                itemsPagedList.TotalPages,
                itemsPagedList.Items);

            return result;
        }

        public CoffeeFortuneTelling GetCoffeeFortuneTelling(Guid id, Guid userId, string role)
        {
            var coffeeFortuneTelling = _coffeeFortuneTellingRepository.GetCoffeeFortuneTelling(id);

            if(role == "FortuneTeller" && coffeeFortuneTelling.FortuneTeller.Id != userId)
            {
                throw new BusinessException("NotAllowed", "Yetkiniz bulunmamaktadır.");
            }

            if (role == "ConsumerUser" && coffeeFortuneTelling.User.Id != userId)
            {
                throw new BusinessException("NotAllowed", "Yetkiniz bulunmamaktadır.");
            }

            return coffeeFortuneTelling;
        }

        public FortuneTellerUser GetFortuneTeller(string userName, Guid id)
        {
            return _coffeeFortuneTellingRepository.GetFortuneTeller(userName, id);
        }
        
        public List<FortuneTellerUser> GetFortuneTellers()
        {
            return _coffeeFortuneTellingRepository.GetFortuneTellers();
        }

        public List<FortuneTellerUser> GetActiveFortuneTellers()
        {
            return _coffeeFortuneTellingRepository.GetActiveFortuneTellers();
        }
    }
}
