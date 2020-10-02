using System;
using System.Collections.Generic;

namespace BackendSocialApp.ViewModels
{
    public class CoffeeFortuneTellingViewModel
    {
        public Guid Id { get; set; }

        public UserViewModel User { get; set; }

        public FortuneTellerViewModel FortuneTeller { get; set; }

        public string Status { get; set; }

        public string FortuneTellerComment { get; set; }

        public string Type { get; set; }

        public DateTime? SubmitDateUtc { get; set; }

        public DateTime? SubmitByFortuneTellerDateUtc { get; set; }

        public DateTime? ReadDateUtc { get; set; }

        public int Point { get; set; }

        public List<string> Pictures { get; set; }

        public string ConsumerGender { get; set; }

        public DateTime? ConsumerBirthDate { get; set; }

        public DateTime? ConsumerBirthTime { get; set; }

        public string ConsumerRelationshipStatus { get; set; }

        public string ConsumerJob { get; set; }

        public int UserStarPoint { get; set; }

        public string FortuneTellingType { get; set; }

        public string UserInput { get; set; }
    }
}
