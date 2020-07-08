using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Domain.Services.Communication;
using BackendSocialApp.Paging;
using BackendSocialApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Services
{
    public class FeedService : IFeedService
    {

        public IFeedRepository _feedRepository;
        public IUnitOfWork _unitOfWork;

        public FeedService(IFeedRepository feedRepository, IUnitOfWork unitOfWork)
        {
            _feedRepository = feedRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IPagedList<MainFeed>> GetFeeds(PageSearchArgs args)
        {
            var itemsPagedList = await _feedRepository.GetFeedsAsync(args);

            var result = new PagedList<MainFeed>(
                itemsPagedList.PageIndex,
                itemsPagedList.PageSize,
                itemsPagedList.TotalCount,
                itemsPagedList.TotalPages,
                itemsPagedList.Items);

            return result;
        }

        public async Task<Survey> GetSurvey(Guid surveyId)
        {
            return await _feedRepository.GetSurveyAsync(surveyId);
        }
    }
}
