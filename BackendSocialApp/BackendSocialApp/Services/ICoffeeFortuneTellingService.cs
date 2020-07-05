using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Services.Communication;
using BackendSocialApp.Paging;
using BackendSocialApp.ViewModels;
using Microsoft.AspNetCore.Http;

namespace BackendSocialApp.Services
{
    public interface ICoffeeFortuneTellingService
    {
        Task<CreateCoffeeFortuneTellingResponse> CreateCoffeeFortuneTellingAsync(CoffeeFortuneTelling coffeeFortuneTelling, List<string> picturePaths);

        Task<SubmitFortuneTellingByFortuneTellerResponse> SubmitFortuneTellingByFortuneTeller(Guid fortuneTellerId, Guid coffeeFortuneTellingId, string comment);

        Task<IPagedList<CoffeeFortuneTelling>> GetUserItems(PageSearchArgs args, Guid userId);

        Task<IPagedList<CoffeeFortuneTelling>> GetFortuneTellerItems(PageSearchArgs args, Guid userId);

        CoffeeFortuneTelling GetCoffeeFortuneTelling(Guid id, Guid userId, string role);

        List<FortuneTellerUser> GetFortuneTellers();

        List<FortuneTellerUser> GetActiveFortuneTellers();
    }
}