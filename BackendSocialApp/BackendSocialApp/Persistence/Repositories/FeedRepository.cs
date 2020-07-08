using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Paging;
using BackendSocialApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendSocialApp.Persistence.Repositories
{
    public class FeedRepository : BaseRepository, IFeedRepository
    {
        public FeedRepository(AppDbContext context) : base(context) { }

        public Task<IPagedList<MainFeed>> GetFeedsAsync(PageSearchArgs args)
        {
            IQueryable<MainFeed> query = _context.MainFeeds;

            var orderByList = new List<Tuple<SortingOption, Expression<Func<MainFeed, object>>>>();

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<MainFeed, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.DESC }, c => c.PublishedDateUtc));
            }

            var pagedList = new PagedList<MainFeed>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, null);

            return Task.FromResult<IPagedList<MainFeed>>(pagedList);
        }

        public Task<Survey> GetSurveyAsync(Guid surveyId)
        {
            return Task.FromResult(
                _context.Surveys
                    .Include(a => a.Items) //.SelectMany(d => d.Answers)
                    .ThenInclude(x => x.Answers)
                    .Include(b => b.Results)
                    .Where(c => c.Id == surveyId)
                    .FirstOrDefault()
            );
        }
    }
}
