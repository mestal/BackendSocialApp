using BackendSocialApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class RegisterNewUserRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Password2 { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public RelationshipStatus? RelationshipStatus { get;set;}

        public GenderType? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? BirthTime { get; set; }

        public string Job { get; set; }
    }
}
