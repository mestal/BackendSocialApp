using AutoMapper;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Paging;
using BackendSocialApp.Requests;
using BackendSocialApp.Services;
using BackendSocialApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendSocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _service;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedController(IFeedService service, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("GetFeeds")]
        public async Task<ActionResult<IPagedList<CoffeeFortuneTellingViewModel>>> GetFeeds(SearchPageRequest request)
        {
            var userClaim = User.Claims.FirstOrDefault(a => a.Type == Constants.ClaimUserId);
            Guid? userId = null;
            if (userClaim != null)
            {
                userId = Guid.Parse(userClaim.Value);
            }

            var items = _service.GetFeeds(request.Args, userId).Result;

            var viewModelList = _mapper.Map<List<MainFeed>, List<FeedViewModel>>(items.Items.ToList());

            var result = new PagedList<FeedViewModel>(items.PageIndex, items.PageSize, items.TotalCount, items.TotalPages, viewModelList);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSurvey")]
        public async Task<ActionResult<SurveyViewModel>> GetSurvey(Guid surveyId)
        {
            var userClaim = User.Claims.FirstOrDefault(a => a.Type == Constants.ClaimUserId);
            Guid? userId = null;
            if(userClaim != null)
            {
                userId = Guid.Parse(userClaim.Value);
            }
            var survey = _service.GetSurvey(surveyId, userId).Result;
            if(survey == null)
            {
                throw new BusinessException("SurveyNotFound", "Kayıt bulunamadı.");
            }

            var viewModel = _mapper.Map<Survey, SurveyViewModel>(survey);

            return Ok(viewModel);
        }

        [HttpGet]
        [Route("GetNews")]
        public async Task<ActionResult<NewsViewModel>> GetNews(Guid newsId)
        {
            var userClaim = User.Claims.FirstOrDefault(a => a.Type == Constants.ClaimUserId);
            Guid? userId = null;
            if (userClaim != null)
            {
                userId = Guid.Parse(userClaim.Value);
            }
            var news = _service.GetNews(newsId, userId).Result;
            if (news == null)
            {
                throw new BusinessException("NewsNotFound", "Kayıt bulunamadı.");
            }

            var viewModel = _mapper.Map<News, NewsViewModel>(news);

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("GetComments")]
        public async Task<ActionResult<List<CommentViewModel>>> GetComments(SearchPageRequest request)
        {
            var items = _service.GetComments(request.Args).Result;

            var viewModelList = _mapper.Map<List<Comment>, List<CommentViewModel>>(items.Items.ToList());

            var result = new PagedList<CommentViewModel>(items.PageIndex, items.PageSize, items.TotalCount, items.TotalPages, viewModelList);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("SubmitComment")]
        public async Task<ActionResult> SubmitComment(SubmitCommentRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if(user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            var id = await _service.SubmitComment(user, request.FeedId, request.Comment);
            return Ok(id);
        }

        [HttpPost]
        [Authorize]
        [Route("RemoveComment")]
        public async Task<ActionResult> RemoveComment(RemoveCommentRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var role = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            await _service.RemoveComment(request.CommentId, role, userId);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("LikeFeed")]
        public async Task<ActionResult> LikeFeed(LikeFeedRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            await _service.LikeFeed(user, request.FeedId, request.LikeType);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("RemoveLikeFeed")]
        public async Task<ActionResult> RemoveLikeFeed(RemoveLikeRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");
            }

            await _service.RemoveLikeFeed(user, request.FeedId, request.LikeType);
            return Ok();
        }
    }
}