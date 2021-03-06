﻿using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Domain.Services.Communication;
using BackendSocialApp.Paging;
using BackendSocialApp.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendSocialApp.Services
{
    public class CoffeeFortuneTellingService : ICoffeeFortuneTellingService
    {

        public ICoffeeFortuneTellingRepository _coffeeFortuneTellingRepository;
        public IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CoffeeFortuneTellingService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IHostingEnvironment _environment;
        private readonly IEmailHelper _emailHelper;

        public CoffeeFortuneTellingService(ICoffeeFortuneTellingRepository coffeeFortuneTellingRepository, IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager, ILogger<CoffeeFortuneTellingService> logger, IUserRepository userRepository,
            IHostingEnvironment environment, IEmailHelper emailHelper)
        {
            _coffeeFortuneTellingRepository = coffeeFortuneTellingRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _logger = logger;
            _userRepository = userRepository;
            _environment = environment;
            _emailHelper = emailHelper;
        }

        public async Task<CreateCoffeeFortuneTellingResponse> CreateCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling, List<string> picturePaths)
        {
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
            coffeeFortuneTelling.User.Point = coffeeFortuneTelling.User.Point - coffeeFortuneTelling.FortuneTeller.CoffeePointPrice;
            var result = await _userManager.UpdateAsync(coffeeFortuneTelling.User);

            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var item in result.Errors)
                {
                    errors = errors + item.Code + " - " + item.Description + ",";
                }

                _logger.LogError("PointDecreaseError: UserName: " + coffeeFortuneTelling.User.UserName + ", Errors: " + errors);

                throw new BusinessException("CanNotConfirm", "Onaylanamadı.");
            }
            await _unitOfWork.CompleteAsync();

            SendNewFalMailToFalci(coffeeFortuneTelling.FortuneTeller);

            return new CreateCoffeeFortuneTellingResponse(coffeeFortuneTelling);
        }

        private void SendNewFalMailToFalci(FortuneTellerUser falci)
        {
            try { 
                var userConfirmationMailTemplatePath = _environment.ContentRootPath + "\\Assets\\Templates\\NewFalToFalci.html";
                var template = System.IO.File.ReadAllText(userConfirmationMailTemplatePath);
                template = template
                    .Replace("[fullName]", falci.FullName);

                _emailHelper.Send(
                    new EmailModel
                    {
                        To = falci.Email,
                        Subject = "Falcım - Yeni Fal",
                        IsBodyHtml = true,
                        Message = template
                    }
                );
            }
            catch(Exception e)
            {
                try
                {
                    _logger.LogError("NewFalMailSendError:  " + falci?.Email + ", Errors: " + e.Message + " - " + e.InnerException?.Message);
                }
                catch
                {

                }
            }
        }

        private void FalCommentedMailToConsumer(ConsumerUser user)
        {
            try
            {
                var userConfirmationMailTemplatePath = _environment.ContentRootPath + "\\Assets\\Templates\\FalCommented.html";
                var template = System.IO.File.ReadAllText(userConfirmationMailTemplatePath);
                template = template
                    .Replace("[fullName]", user.FullName);

                _emailHelper.Send(
                    new EmailModel
                    {
                        To = user.Email,
                        Subject = "Falcım - Falına bakıldı",
                        IsBodyHtml = true,
                        Message = template
                    }
                );
            }
            catch (Exception e)
            {
                try
                {
                    _logger.LogError("FalCommentedMailSendError:  " + user?.Email + ", Errors: " + e.Message + " - " + e.InnerException?.Message);
                }
                catch
                {

                }
            }
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
            fortuneTelling.SubmitByFortuneTellerDateUtc = DateTime.UtcNow;

            _coffeeFortuneTellingRepository.UpdateCoffeeFortuneTelling(fortuneTelling);

            fortuneTelling.FortuneTeller.CoffeFortuneTellingCount++;

            _userRepository.UpdateUser(fortuneTelling.FortuneTeller);

            await _unitOfWork.CompleteAsync();

            FalCommentedMailToConsumer(fortuneTelling.User);

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

        public List<FortuneTellerUser> GetActiveFortuneTellers(FortuneTellingType fortuneTellingType)
        {
            return _coffeeFortuneTellingRepository.GetActiveFortuneTellers(fortuneTellingType);
        }

        public async Task<double> RateFortuneTeller(Guid userId, Guid fortuneTellingId, int star)
        {
            var fortuneTelling = _coffeeFortuneTellingRepository.GetCoffeeFortuneTelling(fortuneTellingId);

            if (fortuneTelling == null)
            {
                throw new BusinessException("FortuneTellingNotFound", "Fal bulunamadı.");
            }

            if(fortuneTelling.UserStarPoint != 0)
            {
                throw new BusinessException("FortuneTellingAlreadyRated", "Zaten puan verilmiş.");
            }

            if (fortuneTelling.Status != CoffeeFortuneTellingStatus.SubmittedByFortuneTeller)
            {
                throw new BusinessException("NotValidStatus", "Henüz yorumlanmamış.");
            }

            if (fortuneTelling.User.Id != userId)
            {
                throw new BusinessException("NotAllowedAction", "Bu fal a puan veremezsiniz.");
            }

            if (star < 1 || star > 5)
            {
                throw new BusinessException("RateStarNotValid", "Verilen puan uygun değil.");
            }

            fortuneTelling.UserStarPoint = star;

            _coffeeFortuneTellingRepository.UpdateCoffeeFortuneTelling(fortuneTelling);

            fortuneTelling.FortuneTeller.UserStarPointCount++;
            fortuneTelling.FortuneTeller.UserStarPointTotal = fortuneTelling.FortuneTeller.UserStarPointTotal + star;
            _userRepository.UpdateUser(fortuneTelling.FortuneTeller);

            await _unitOfWork.CompleteAsync();

            return Math.Round((double)fortuneTelling.FortuneTeller.UserStarPointTotal / (double)fortuneTelling.FortuneTeller.UserStarPointCount, 1);
        }
    }
}
