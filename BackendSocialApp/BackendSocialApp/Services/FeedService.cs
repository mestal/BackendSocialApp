using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Paging;
using System;
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

        public async Task<IPagedList<MainFeed>> GetFeeds(PageSearchArgs args, Guid? userId)
        {
            var itemsPagedList = await _feedRepository.GetFeedsAsync(args, userId);

            var result = new PagedList<MainFeed>(
                itemsPagedList.PageIndex,
                itemsPagedList.PageSize,
                itemsPagedList.TotalCount,
                itemsPagedList.TotalPages,
                itemsPagedList.Items);

            return result;
        }

        public async Task<Survey> GetSurvey(Guid surveyId, Guid? userId)
        {
            var result = await _feedRepository.GetSurveyAsync(surveyId);
            if (result == null) { return result; }
            if (userId.HasValue) { 
                var like = await _feedRepository.GetFeedLikedDislikedAsync(surveyId, userId.Value);
                if(like != null)
                {
                    result.LikedType = like.LikeType;
                }
            }

            return result;
        }

        public async Task<News> GetNews(Guid newsId, Guid? userId)
        {
            var result = await _feedRepository.GetNewsAsync(newsId);
            if(result == null) { return result; }
            if (userId.HasValue)
            {
                var like = await _feedRepository.GetFeedLikedDislikedAsync(newsId, userId.Value);
                if (like != null)
                {
                    result.LikedType = like.LikeType;
                }
            }

            return result;
        }

        public async Task<IPagedList<Comment>> GetComments(PageSearchArgs args)
        {
            var itemsPagedList = await _feedRepository.GetCommentsAsync(args);

            var result = new PagedList<Comment>(
                itemsPagedList.PageIndex,
                itemsPagedList.PageSize,
                itemsPagedList.TotalCount,
                itemsPagedList.TotalPages,
                itemsPagedList.Items);

            return result;
        }

        public async Task<Guid> SubmitComment(ApplicationUser user, Guid refId, string comment)
        {
            var feed = _feedRepository.GetFeedAsync(refId);

            if (feed == null)
            {
                throw new BusinessException("FeedNotFound", "Kayıt bulunamadı.");
            }

            var newComment = new Comment();
            newComment.CreateDate = DateTime.UtcNow;
            newComment.RefId = refId;
            newComment.User = user;
            newComment.UserComment = comment;

            await _feedRepository.SaveCommentAsync(newComment);
            await _feedRepository.ChangeCommentCountAsync(refId, 1);
            await _unitOfWork.CompleteAsync();
            return newComment.Id;
        }

        public async Task RemoveComment(Guid commentId, string role, Guid userId)
        {
            var comment = await _feedRepository.GetCommentAsync(commentId);

            if (comment == null)
            {
                throw new BusinessException("CommentNotFound", "Kayıt bulunamadı.");
            }

            if(comment.User.Id != userId && role != Constants.RoleAdmin)
            {
                throw new BusinessException("NotAllowed", "Yetkiniz bulunmamaktadır.");
            }

            _feedRepository.RemoveComment(comment);
            await _feedRepository.ChangeCommentCountAsync(comment.RefId, -1);
            await _unitOfWork.CompleteAsync();
        }

        public async Task LikeFeed(ApplicationUser user, Guid refId, int likeType)
        {
            if(likeType != 1 && likeType != 2)
            {
                throw new BusinessException("WrongLikeType", "Yanlış beğenme tipi.");
            }

            var feed = _feedRepository.GetFeedAsync(refId);

            if (feed == null)
            {
                throw new BusinessException("FeedNotFound", "Kayıt bulunamadı.");
            }

            var like = await _feedRepository.GetFeedLikedDislikedAsync(refId, user.Id);

            if (like == null) {

                like = new Like();
                like.RefId = refId;
                like.UserId = user.Id;
                like.LikeType = likeType;
                like.Date = DateTime.UtcNow;

                await _feedRepository.SaveLikeAsync(like);

                if (likeType == 1)
                {
                    await _feedRepository.ChangeLikeCountAsync(refId, 1);
                }
                else
                {
                    await _feedRepository.ChangeDislikeCountAsync(refId, 1);
                }
            }
            else
            {
                if(like.LikeType == likeType)
                {
                    return;
                }

                like.LikeType = likeType;

                if (likeType == 1)
                {
                    await _feedRepository.ChangeLikeCountAsync(refId, 1);
                    await _feedRepository.ChangeDislikeCountAsync(refId, -1);
                }
                else
                {
                    await _feedRepository.ChangeLikeCountAsync(refId, -1);
                    await _feedRepository.ChangeDislikeCountAsync(refId, 1);
                }

                _feedRepository.UpdateLike(like);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveLikeFeed(ApplicationUser user, Guid refId, int likeType)
        {
            if (likeType != 1 && likeType != 2)
            {
                throw new BusinessException("WrongLikeType", "Yanlış beğenme tipi.");
            }

            var like = await _feedRepository.GetLikeAsync(refId, user.Id, likeType);

            if (like == null)
            {
                throw new BusinessException("LikeNotFound", "Kayıt bulunamadı.");
            }

            _feedRepository.RemoveLike(like);
            if (likeType == 1)
            {
                await _feedRepository.ChangeLikeCountAsync(refId, -1);
            }
            else
            {
                await _feedRepository.ChangeDislikeCountAsync(refId, -1);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
