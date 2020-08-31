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

        public async Task<News> GetNews(Guid newsId)
        {
            return await _feedRepository.GetNewsAsync(newsId);
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

        public async Task SubmitComment(ApplicationUser user, Guid refId, string comment)
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
            var feed = _feedRepository.GetFeedAsync(refId);

            if (feed == null)
            {
                throw new BusinessException("FeedNotFound", "Kayıt bulunamadı.");
            }

            var newLike = new Like();
            newLike.RefId = refId;
            newLike.UserId = user.Id;
            newLike.LikeType = likeType;

            await _feedRepository.SaveLikeAsync(newLike);
            if(likeType == 0) { 
                await _feedRepository.ChangeLikeCountAsync(refId, 1);
            }
            else
            {
                await _feedRepository.ChangeDislikeCountAsync(refId, 1);
            }
            await _unitOfWork.CompleteAsync();
        }

        public async Task RemoveLikeFeed(ApplicationUser user, Guid refId, int likeType)
        {
            var like = await _feedRepository.GetLikeAsync(refId, user.Id, likeType);

            if (like == null)
            {
                throw new BusinessException("LikeNotFound", "Kayıt bulunamadı.");
            }

            _feedRepository.RemoveLike(like);
            if (likeType == 0)
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
