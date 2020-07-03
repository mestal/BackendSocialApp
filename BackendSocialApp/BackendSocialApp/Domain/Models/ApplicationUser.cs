using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSocialApp.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public  UserStatus Status { get; set; }

        public string PicturePath { get; set; }
    }

    public class AdminUser : ApplicationUser
    {
    }

    public class ConsumerUser : ApplicationUser
    {
        public int Point { get; set; }
    }

    public class FortuneTellerUser : ApplicationUser
    {
        public int CoffeePointPrice { get; set; }

        public int CoffeFortuneTellingCount { get; set; }

    }
}
