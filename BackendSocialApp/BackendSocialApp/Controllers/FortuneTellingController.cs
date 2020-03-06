using AutoMapper;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Services.Communication;
using BackendSocialApp.Paging;
using BackendSocialApp.Requests;
using BackendSocialApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [Authorize(Roles = "Consumer")]
        [Route("CreateCoffeeFortuneTelling")]
        public async Task<ActionResult> CreateCoffeeFortuneTelling([FromForm]CreateCoffeeFortuneTellingRequest request)
        {
            var newCoffeeFortuneTelling = _mapper.Map<CreateCoffeeFortuneTellingRequest, CoffeeFortuneTelling>(request);
            var userId = User.Claims.First(a => a.Type == "UserID").Value;
            var userRole = User.Claims.First(a => a.Type == "Role").Value;

            if (userId != request.UserId.ToString() && userRole != "Admin")
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
        [Authorize(Roles = "Falci")]
        [Route("SubmitByFortuneTeller")]
        public async Task<ActionResult> SubmitByFortuneTeller(SubmitByFortuneTellerRequest request)
        {
            var fortuneTellerId = new Guid(User.Claims.First(a => a.Type == "UserID").Value);
            await _service.SubmitFortuneTellingByFortuneTeller(fortuneTellerId, request.CoffeeFortuneTellingId, request.Comment);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Consumer")]
        [Route("GetUserItems")]
        public async Task<ActionResult<IPagedList<CoffeeFortuneTelling>>> GetUserItems(SearchPageRequest request)
        {
            var userId = new Guid(User.Claims.First(a => a.Type == "UserID").Value);
            return Ok(_service.GetUserItems(request.Args, userId));
        }

        [HttpPost]
        [Authorize(Roles = "Falci")]
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
            var userId = new Guid(User.Claims.First(a => a.Type == "UserID").Value);
            var userRole = User.Claims.First(a => a.Type == "Role").Value;

            var result = _service.GetCoffeeFortuneTelling(id, userId, userRole);

            return new GetFortuneTellingByIdResponse { CoffeeFortuneTelling = result };
        }
    }
}