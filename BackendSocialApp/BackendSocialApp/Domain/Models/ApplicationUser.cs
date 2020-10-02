using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSocialApp.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public  UserStatus Status { get; set; }

        public string PicturePath { get; set; }

        public ConnectionStatus? ConnectionStatus { get; set; }

        public GenderType? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }

        public bool? IsTestUser { get; set; }

        public DateTime CreateDate { get; set; }
    }

    public class AdminUser : ApplicationUser
    {
    }

    public class ConsumerUser : ApplicationUser
    {
        public int Point { get; set; }

        public DateTime? BirthTime { get; set; }

        public RelationshipStatus? RelationshipStatus { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Job { get; set; }
    }

    public class FortuneTellerUser : ApplicationUser
    {
        public int CoffeePointPrice { get; set; }

        public int CoffeFortuneTellingCount { get; set; }

        public int UserStarPointCount { get; set; }

        public int UserStarPointTotal { get; set; }

        public List<FortuneTellerUserFalType> FalTypes { get; set; }

    }

    public class FortuneTellerUserFalType
    {
        public Guid Id { get; set; }

        public FortuneTellerUser FortuneTeller { get; set; }

        public int FortunrTellingType { get; set; }

    }

    public class ContentCreatorUser : ApplicationUser
    {

    }
}
