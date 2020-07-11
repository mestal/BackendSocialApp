using AutoMapper;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Services.Communication;
using BackendSocialApp.Paging;
using BackendSocialApp.Requests;
using BackendSocialApp.Services;
using BackendSocialApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public FortuneTellingController(ICoffeeFortuneTellingService service, IMapper mapper, IHostingEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _mapper = mapper;
            _environment = environment;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = Constants.RoleConsumer)]
        [Route("CreateCoffeeFortuneTelling")]
        public async Task<ActionResult> CreateCoffeeFortuneTelling([FromForm]CreateCoffeeFortuneTellingRequest request)
        {
            var newCoffeeFortuneTelling = _mapper.Map<CreateCoffeeFortuneTellingRequest, CoffeeFortuneTelling>(request);
            var userId = User.Claims.First(a => a.Type == Constants.ClaimUserId).Value;
            var userRole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;

            if (userId != request.UserId.ToString() && userRole != Constants.RoleAdmin)
            {
                throw new Exception("You can not create for another user.");
            }

            var user = (ConsumerUser)await _userManager.FindByIdAsync(request.UserId.ToString());

            newCoffeeFortuneTelling.User = user ?? throw new Exception("User Not Found");

            var fortuneTeller = (FortuneTellerUser)await _userManager.FindByIdAsync(request.FortuneTellerId.ToString());
            newCoffeeFortuneTelling.FortuneTeller = fortuneTeller ?? throw new Exception("User Not Found");

            if (request.Pictures.Count == 0)
            {
                throw new Exception("Pictures must be send.");
            }

            var folderPath = _environment.ContentRootPath + "\\Upload\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var pictureUrls = new List<string>();
            foreach (var file in request.Pictures)
            {
                if(file.ContentType.Length > 30)
                {
                    throw new Exception("Wrong Content-Type");
                }
                var fileName = Guid.NewGuid() + "." + file.ContentType.Replace('/', '.');
                var fileFullPath = folderPath + fileName;
                pictureUrls.Add(fileName);
                using (FileStream fileStream = System.IO.File.Create(fileFullPath))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
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
            await _service.SubmitFortuneTellingByFortuneTeller(fortuneTellerId, request.CoffeeFortuneTellingId, request.Comment);
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
            var userId = new Guid(User.Claims.First(a => a.Type == "UserID").Value);
            return Ok(_service.GetFortuneTellerItems(request.Args, userId));
        }

        [HttpGet]
        [Authorize]
        [Route("GetFortuneTellingById")]
        public GetFortuneTellingByIdResponse GetFortuneTellingById(Guid id)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == Constants.ClaimUserId).Value);
            var userRole = User.Claims.First(a => a.Type == ClaimTypes.Role).Value;

            var result = _service.GetCoffeeFortuneTelling(id, userId, userRole);

            return new GetFortuneTellingByIdResponse { CoffeeFortuneTelling = result };
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
    }
}