using AutoMapper;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Paging;
using BackendSocialApp.Requests;
using BackendSocialApp.Services;
using BackendSocialApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _service;
        private readonly IMapper _mapper;

        public FeedController(IFeedService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetFeeds")]
        public async Task<ActionResult<IPagedList<CoffeeFortuneTellingViewModel>>> GetFeeds(SearchPageRequest request)
        {
            var items = _service.GetFeeds(request.Args).Result;

            var viewModelList = _mapper.Map<List<MainFeed>, List<FeedViewModel>>(items.Items.ToList());

            var result = new PagedList<FeedViewModel>(items.PageIndex, items.PageSize, items.TotalCount, items.TotalPages, viewModelList);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetSurvey")]
        public async Task<ActionResult<SurveyViewModel>> GetSurvey(Guid surveyId)
        {
            var survey = _service.GetSurvey(surveyId).Result;
            if(survey == null)
            {
                throw new Exception("Survey not found");
            }

            var viewModel = _mapper.Map<Survey, SurveyViewModel>(survey);

            return Ok(viewModel);
        }
    }
}