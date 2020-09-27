using BackendSocialApp.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class RateFortuneTellerRequest
    {
        public int Star { get; set; }

        public Guid FortuneTellingId { get; set; }
    }
}
