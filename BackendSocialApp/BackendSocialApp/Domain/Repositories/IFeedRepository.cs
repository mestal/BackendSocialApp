using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Paging;
//using BackendSocialApp.Domain.Queries;

namespace BackendSocialApp.Domain.Repositories
{
    public interface IFeedRepository
    {
        Task<IPagedList<MainFeed>> GetFeedsAsync(PageSearchArgs args);
    }
}
