using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Paging;
using BackendSocialApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public Task<News> GetNewsAsync(Guid newsId)
        {
            return Task.FromResult(
                _context.NewsList
                    .Include(a => a.Items)
                    .Where(c => c.Id == newsId)
                    .FirstOrDefault()
            );
        }

        public Task<MainFeed> GetFeedAsync(Guid feedId)
        {
            return Task.FromResult(
                _context.MainFeeds
                    .Where(c => c.Id == feedId)
                    .FirstOrDefault()
            );
        }

        public Task<Like> GetLikeAsync(Guid feedId, Guid userId, int likeType)
        {
            return Task.FromResult(
                _context.Likes
                    .Where(c => c.RefId == feedId && c.UserId == userId && c.LikeType == likeType)
                    .FirstOrDefault()
            );
        }

        public Task<Comment> GetCommentAsync(Guid commentId)
        {
            return Task.FromResult(
                _context.Comments.Include(a => a.User)
                    .Where(c => c.Id == commentId)
                    .FirstOrDefault()
            );
        }

        public void RemoveComment(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public Task<IPagedList<Comment>> GetCommentsAsync(PageSearchArgs args)
        {
            if(args == null || args.FilteringOptions == null || !args.FilteringOptions.Any(a => a.Field == "RefId"))
            {
                throw new BusinessException("MissingFilter", "RefId eksik.");
            }

            IQueryable<Comment> query = _context.Comments.Include(a => a.User);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<Comment, object>>>>();

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Comment, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.DESC }, c => c.CreateDate));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<Comment, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "RefId":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Comment, bool>>>(filteringOption, c => c.RefId == Guid.Parse(filteringOption.Value.ToString())));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<Comment>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Comment>>(pagedList);
        }

        public Task SaveCommentAsync(Comment comment)
        {
            return _context.Comments.AddAsync(comment);
        }

        public Task SaveLikeAsync(Like like)
        {
            return _context.Likes.AddAsync(like);
        }

        public void RemoveLike(Like like)
        {
            _context.Likes.Remove(like);
        }

        public Task<int> ChangeCommentCountAsync(Guid feedId, int number)
        {
            object[] parameters = {
                new SqlParameter("@Number", number),
                new SqlParameter("@FeedId", feedId)
            };
            return _context.Database.ExecuteSqlCommandAsync(new RawSqlString("Update MainFeeds Set CommentCount = CommentCount + @Number Where Id = @FeedId"), parameters);
        }

        public Task<int> ChangeLikeCountAsync(Guid feedId, int number)
        {
            object[] parameters = {
                new SqlParameter("@Number", number),
                new SqlParameter("@FeedId", feedId)
            };
            return _context.Database.ExecuteSqlCommandAsync(new RawSqlString("Update MainFeeds Set LikeCount = LikeCount + @Number Where Id = @FeedId"), parameters);
        }

        public Task<int> ChangeDislikeCountAsync(Guid feedId, int number)
        {
            object[] parameters = {
                new SqlParameter("@Number", number),
                new SqlParameter("@FeedId", feedId)
            };
            return _context.Database.ExecuteSqlCommandAsync(new RawSqlString("Update MainFeeds Set DislikeCount = DislikeCount + @Number Where Id = @FeedId"), parameters);
        }
    }
}
