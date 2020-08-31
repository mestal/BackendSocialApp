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
        Task<IPagedList<MainFeed>> GetFeeds(PageSearchArgs args, Guid? userId);

        Task<Survey> GetSurvey(Guid surveyId, Guid? userId);

        Task<News> GetNews(Guid newsId, Guid? userId);

        Task<IPagedList<Comment>> GetComments(PageSearchArgs args);

        Task<Guid> SubmitComment(ApplicationUser user, Guid refId, string comment);

        Task RemoveComment(Guid commentId, string role, Guid userId);

        Task LikeFeed(ApplicationUser user, Guid refId, int likeType);

        Task RemoveLikeFeed(ApplicationUser user, Guid refId, int likeType);
    }
}