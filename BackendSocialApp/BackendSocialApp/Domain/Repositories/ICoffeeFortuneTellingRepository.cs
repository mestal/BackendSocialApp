using BackendSocialApp.Domain.Models;
using BackendSocialApp.Paging;
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

        CoffeeFortuneTelling GetCoffeeFortuneTelling(Guid id);

        List<FortuneTellerUser> GetFortuneTellers();

        List<FortuneTellerUser> GetActiveFortuneTellers();

        List<FortuneTellerUser> GetActiveFortuneTellers(FortuneTellingType fortuneTellingType);

        void UpdateCoffeeFortuneTelling(CoffeeFortuneTelling coffeeFortuneTelling);

        Task<IPagedList<CoffeeFortuneTelling>> GetUserItemsAsync(PageSearchArgs args, Guid userId);

        Task<IPagedList<CoffeeFortuneTelling>> GetFortuneTellerItemsAsync(PageSearchArgs args, Guid userId);

        FortuneTellerUser GetFortuneTeller(string userName, Guid id);
    }
}
