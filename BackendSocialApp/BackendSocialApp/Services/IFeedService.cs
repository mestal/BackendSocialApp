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
    public interface IFeedService
    {
        Task<IPagedList<MainFeed>> GetFeeds(PageSearchArgs args);

        Task<Survey> GetSurvey(Guid surveyId);
    }
}