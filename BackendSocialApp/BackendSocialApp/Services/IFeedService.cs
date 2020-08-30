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

        Task<News> GetNews(Guid newsId);

        Task<IPagedList<Comment>> GetComments(PageSearchArgs args);

        Task SubmitComment(ApplicationUser user, Guid refId, string comment);

        Task RemoveComment(Guid commentId, string role, Guid userId);

        Task LikeFeed(ApplicationUser user, Guid refId, int likeType);

        Task RemoveLikeFeed(ApplicationUser user, Guid refId, int likeType);
    }
}