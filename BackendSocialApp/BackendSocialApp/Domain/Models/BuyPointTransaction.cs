using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Models
{
    public class BuyPointTransaction
    {
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime? SubmitDateUtc { get; set; }

        public int BoughtPoint { get; set; }
    }
}
