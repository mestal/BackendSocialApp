using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class SubmitByFortuneTellerRequest
    {
        public string Comment { get; set; }

        public Guid CoffeeFortuneTellingId { get; set; }
    }
}
