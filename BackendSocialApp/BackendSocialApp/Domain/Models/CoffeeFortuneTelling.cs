using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Models
{
    public class CoffeeFortuneTelling
    {
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public ApplicationUser FortuneTeller { get; set; }

        public CoffeeFortuneTellingStatus Status { get; set; }

        public string FortuneTellerComment { get; set; }

        public CoffeeFortuneTellingType Type { get; set; }

        public List<CoffeeFortuneTellingPicture> Pictures { get; set; }

        public DateTime? SubmitDateUtc { get; set; }

        public DateTime? SubmitByFortuneTellerDateUtc { get; set; }

        public DateTime? ReadDateUtc { get; set; }
    }

    public class CoffeeFortuneTellingPicture
    {
        public Guid Id { get; set; }

        public string Path { get; set; }

        public CoffeeFortuneTelling CoffeeFortuneTelling { get; set; }
    }
}
