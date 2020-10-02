using BackendSocialApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class SubmitCoffeeFortuneTellingRequest
    {
        public List<IFormFile> Pictures { get; set; }

        public Guid FortuneTellerId { get; set; }

        public CoffeeFortuneTellingType Type { get; set; }

        public FortuneTellingType FortuneTellingType { get; set; }

        public string UserInput { get; set; }
    }
}
