using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Services.Communication
{
    public class SubmitFortuneTellingByFortuneTellerResponse : BaseResponse
    {
        public SubmitFortuneTellingByFortuneTellerResponse() : base() { }

        public SubmitFortuneTellingByFortuneTellerResponse(string message) : base(message) { }
    }
}
