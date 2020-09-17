using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.ViewModels
{
    public class PointViewModel
    {
        public Guid Id { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
