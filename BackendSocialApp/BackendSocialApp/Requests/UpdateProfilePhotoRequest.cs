using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class UpdateProfilePhotoRequest
    {
        public IFormFile Photo { get; set; }
    }
}
