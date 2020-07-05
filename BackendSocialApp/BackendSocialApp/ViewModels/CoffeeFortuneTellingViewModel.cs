using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.ViewModels
{
    public class CoffeeFortuneTellingViewModel
    {
        public Guid Id { get; set; }

        public UserViewModel User { get; set; }

        public UserViewModel FortuneTeller { get; set; }

        public string Status { get; set; }

        public string FortuneTellerComment { get; set; }

        public string Type { get; set; }

        public DateTime? SubmitDateUtc { get; set; }

        public DateTime? SubmitByFortuneTellerDateUtc { get; set; }

        public DateTime? ReadDateUtc { get; set; }

        public int Point { get; set; }
    }
}
