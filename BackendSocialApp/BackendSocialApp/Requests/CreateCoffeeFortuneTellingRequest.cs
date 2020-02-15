using BackendSocialApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class CreateCoffeeFortuneTellingRequest
    {
        public CoffeeFortuneTellingStatus Type { get; set; }

        public List<IFormFile> Pictures { get; set; }

        public Guid UserId { get; set; }

        public Guid FortuneTellerId { get; set; }
    }
}
