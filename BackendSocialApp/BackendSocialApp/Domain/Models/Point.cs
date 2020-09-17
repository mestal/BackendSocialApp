using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Models
{
    public class Point
    {
        public Guid Id { get; set; }

        public PointType PointType { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ProductId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Description { get; set; }

        public int PointValue { get; set; }

    }
}
