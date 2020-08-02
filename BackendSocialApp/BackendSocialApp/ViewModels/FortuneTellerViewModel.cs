using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.ViewModels
{
    public class FortuneTellerViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string ConnectionStatus { get; set; }

        public string PicturePath { get; set; }
    }
}
