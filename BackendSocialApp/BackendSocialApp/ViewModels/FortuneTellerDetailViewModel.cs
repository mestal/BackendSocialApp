using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.ViewModels
{
    public class FortuneTellerDetailViewModel: FortuneTellerViewModel
    {
        public int CoffeePointPrice { get; set; }

        public int CoffeFortuneTellingCount { get; set; }

        public string Gender { get; set; }

        public DateTime? BirthDate { get; set; }

    }
}
