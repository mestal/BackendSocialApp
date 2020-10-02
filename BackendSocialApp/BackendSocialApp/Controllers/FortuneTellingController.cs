using AutoMapper;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Services.Communication;
using BackendSocialApp.Paging;
using BackendSocialApp.Requests;
using BackendSocialApp.Services;
using BackendSocialApp.Tools;
using BackendSocialApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendSocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FortuneTellingController : ControllerBase
    {
        private readonly ICoffeeFortuneTellingService _service;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public FortuneTellingController(ICoffeeFortuneTellingService service, IMapper mapper, 
            IHostingEnvironment environment, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _service = service;
            _mapper = mapper;
            _environment = environment;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleConsumer)]
        [Route("SubmitCoffeeFortuneTelling")]
        public async Task<ActionResult> SubmitCoffeeFortuneTelling([FromForm]SubmitCoffeeFortuneTellingRequest request)
        {
            var newCoffeeFortuneTelling = _mapper.Map<SubmitCoffeeFortuneTellingRequest, CoffeeFortuneTelling>(request);
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var userRole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;

            var user = (await _userManager.FindByIdAsync(userId.ToString())) as ConsumerUser;

            newCoffeeFortuneTelling.User = user ?? throw new BusinessException("UserNotFound", "Kullanıcı bulunamadı.");

            newCoffeeFortuneTelling.ConsumerBirthDate = user.BirthDate;
            newCoffeeFortuneTelling.ConsumerBirthTime = user.BirthTime;
            newCoffeeFortuneTelling.ConsumerGender = user.Gender;
            newCoffeeFortuneTelling.ConsumerJob = user.Job;
            newCoffeeFortuneTelling.ConsumerRelationshipStatus = user.RelationshipStatus;
            newCoffeeFortuneTelling.FortuneTellingType = request.FortuneTellingType;
            newCoffeeFortuneTelling.UserInput = request.UserInput;

            var fortuneTeller = (await _userService.GetFortuneTellerUser(request.FortuneTellerId));
            newCoffeeFortuneTelling.FortuneTeller = fortuneTeller ?? throw new BusinessException("FortuneTellerNotFound", "Falcı bulunamadı. ");

            if(!fortuneTeller.FalTypes.Exists(a => a.FortunrTellingType == (int)request.FortuneTellingType))
            {
                throw new BusinessException("NotDefinedFalTypeForThisFortuneTeller", "Bu falcımız bu tip fala bakmamaktadır.");
            }

            if (request.FortuneTellingType != FortuneTellingType.DreamInterpretation)
            {
                if(request.Pictures == null || request.Pictures.Count == 0) { 
                    throw new BusinessException("PicturesMustBeSend", "Fotoğraf gönderilmeli.");
                }

                if (!string.IsNullOrEmpty(request.UserInput) && !string.IsNullOrWhiteSpace(request.UserInput))
                {
                    throw new BusinessException("UserInputMustNotBeFilled", "Kullanıcı açıklama girmemeli.");
                }
            }
            else
            {
                if (request.Pictures != null)
                {
                    throw new BusinessException("PicturesMustNotBeSend", "Fotoğraf gönderilmemeli.");
                }

                if(string.IsNullOrEmpty(request.UserInput) || string.IsNullOrWhiteSpace(request.UserInput))
                {
                    throw new BusinessException("UserInputMustBeFilled", "Kullanıcı rüyası girilmeli.");
                }
            }

            var folderPath = _environment.ContentRootPath + "\\Assets\\CoffeeFortuneTellingPictures\\";

            var folderExists = true;
            if (!Directory.Exists(folderPath))
            {
                folderExists = false;
                Directory.CreateDirectory(folderPath);
            }

            var pictureUrls = new List<string>();
            var fileFullPaths = new List<string>();
            if (request.Pictures != null)
            {
                foreach (var file in request.Pictures)
                {
                    if(file.ContentType.Length > 30)
                    {
                        throw new Exception("Wrong Content-Type");
                    }
                    var fileName = Guid.NewGuid() + "." + file.ContentType.Replace('/', '.');
                    var fileFullPath = folderPath + fileName;
                    pictureUrls.Add(fileName);
                    fileFullPaths.Add(fileFullPath);

                    using (var fileStream = new FileStream(fileFullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            await _service.CreateCoffeeFortuneTellingAsync(newCoffeeFortuneTelling, pictureUrls);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleFalci)]
        [Route("SubmitByFortuneTeller")]
        public async Task<ActionResult> SubmitByFortuneTeller(SubmitByFortuneTellerRequest request)
        {
            var fortuneTellerId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            await _service.SubmitFortuneTellingByFortuneTeller(fortuneTellerId, request.FortuneTellingId, request.Comment);
            return Ok();
        }

        [HttpPost]
        [Authorize (Roles = Constants.RoleConsumer)]
        [Route("GetUserItems")]
        public async Task<ActionResult<IPagedList<CoffeeFortuneTellingViewModel>>> GetUserItems(SearchPageRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var userItems = _service.GetUserItems(request.Args, userId).Result;

            var viewModelList = _mapper.Map<List<CoffeeFortuneTelling>, List<CoffeeFortuneTellingViewModel>>(userItems.Items.ToList());

            var result = new PagedList<CoffeeFortuneTellingViewModel>(userItems.PageIndex, userItems.PageSize, userItems.TotalCount, userItems.TotalPages, viewModelList);
            return Ok(result);
        }

        [HttpPost]
        [Authorize (Roles = Constants.RoleFalci)]
        [Route("GetFortuneTellerItems")]
        public async Task<ActionResult<IPagedList<CoffeeFortuneTelling>>> GetFortuneTellerItems(SearchPageRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var userItems = _service.GetFortuneTellerItems(request.Args, userId).Result;

            var viewModelList = _mapper.Map<List<CoffeeFortuneTelling>, List<CoffeeFortuneTellingViewModel>>(userItems.Items.ToList());

            var result = new PagedList<CoffeeFortuneTellingViewModel>(userItems.PageIndex, userItems.PageSize, userItems.TotalCount, userItems.TotalPages, viewModelList);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("GetFortuneTellingById")]
        public CoffeeFortuneTellingViewModel GetFortuneTellingById(Guid id)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var userRole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;

            var result = _service.GetCoffeeFortuneTelling(id, userId, userRole);
            var viewModel = _mapper.Map<CoffeeFortuneTelling, CoffeeFortuneTellingViewModel>(result);

            return viewModel;
        }

        [HttpGet]
        [Route("GetFortuneTeller")]
        public FortuneTellerDetailViewModel GetFortuneTeller(string userName, Guid id)
        {
            var result = _service.GetFortuneTeller(userName, id);

            if(result == null)
            {
                throw new BusinessException("RecordNotFound", "Kayıt bulunamadı.");
            }
            var viewModel = _mapper.Map<FortuneTellerUser, FortuneTellerDetailViewModel>(result);

            return viewModel;
        }

        [HttpGet]
        [Authorize(Roles = Constants.RoleAdmin)]
        [Route("GetFortuneTellers")]
        public object GetFortuneTellers()
        {
            return _service.GetFortuneTellers();
        }

        [HttpGet]
        [Route("GetActiveFortuneTellers")]
        public object GetActiveFortuneTellers()
        {
            return _mapper.Map<List<FortuneTellerUser>, List<FortuneTellerViewModel>>(_service.GetActiveFortuneTellers());
        }

        [HttpPost]
        [Route("GetActiveFortuneTellers")]
        public object GetActiveFortuneTellers(GetActiveFortuneTellersRequest request)
        {
            return _mapper.Map<List<FortuneTellerUser>, List<FortuneTellerViewModel>>(_service.GetActiveFortuneTellers(request.FortuneTellingType));
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleConsumer)]
        [Route("RateFortuneTeller")]
        public async Task<ActionResult> RateFortuneTeller(RateFortuneTellerRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);

            var result = await _service.RateFortuneTeller(userId, request.FortuneTellingId, request.Star);

            return Ok(result);
        }
    }
}