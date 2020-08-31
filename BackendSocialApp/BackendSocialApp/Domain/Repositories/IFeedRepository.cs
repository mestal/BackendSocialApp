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
        Task<IPagedList<MainFeed>> GetFeedsAsync(PageSearchArgs args, Guid? userId);

        Task<Survey> GetSurveyAsync(Guid surveyId);

        Task<MainFeed> GetFeedAsync(Guid feedId);

        Task<News> GetNewsAsync(Guid newsId);

        Task<IPagedList<Comment>> GetCommentsAsync(PageSearchArgs args);

        Task SaveCommentAsync(Comment comment);

        Task SaveLikeAsync(Like like);

        Task<int> ChangeCommentCountAsync(Guid feedId, int number);

        Task<int> ChangeLikeCountAsync(Guid feedId, int number);

        Task<int> ChangeDislikeCountAsync(Guid feedId, int number);

        void RemoveComment(Comment comment);

        void RemoveLike(Like like);

        Task<Comment> GetCommentAsync(Guid commentId);

        Task<Like> GetLikeAsync(Guid feedId, Guid userId, int likeType);

        Task<Like> GetFeedLikedDislikedAsync(Guid feedId, Guid userId);

        void UpdateLike(Like like);

    }
}
