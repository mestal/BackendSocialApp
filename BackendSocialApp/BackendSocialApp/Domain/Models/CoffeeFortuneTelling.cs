using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendSocialApp.Domain.Models
{
    public class CoffeeFortuneTelling
    {
        public Guid Id { get; set; }

        public ConsumerUser User { get; set; }

        public FortuneTellerUser FortuneTeller { get; set; }

        public CoffeeFortuneTellingStatus Status { get; set; }

        public string FortuneTellerComment { get; set; }

        public CoffeeFortuneTellingType Type { get; set; }

        public List<CoffeeFortuneTellingPicture> Pictures { get; set; }

        public DateTime? SubmitDateUtc { get; set; }

        public DateTime? SubmitByFortuneTellerDateUtc { get; set; }

        public DateTime? ReadDateUtc { get; set; }

        public int Point { get; set; }

        public GenderType? ConsumerGender { get; set; }

        public DateTime? ConsumerBirthDate { get; set; }

        public DateTime? ConsumerBirthTime { get; set; }

        public RelationshipStatus? ConsumerRelationshipStatus { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ConsumerJob { get; set; }

    }

    public class CoffeeFortuneTellingPicture
    {
        public Guid Id { get; set; }

        public string Path { get; set; }

        public CoffeeFortuneTelling CoffeeFortuneTelling { get; set; }
    }
}
