using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }
    }

    public class UserInfoViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime BirthTime { get; set; }

        public string Gender { get; set; }

        public string PicturePath { get; set; }

        public int Point { get; set; }

        public string Email { get; set; }

    }
}
