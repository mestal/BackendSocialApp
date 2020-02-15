using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Services.Communication
{
    public class CreateCoffeeFortuneTellingResponse : BaseResponse<CoffeeFortuneTelling>
    {
        public CreateCoffeeFortuneTellingResponse(CoffeeFortuneTelling coffeeFortuneTelling) : base(coffeeFortuneTelling) { }

        public CreateCoffeeFortuneTellingResponse(string message) : base(message) { }
    }
}
