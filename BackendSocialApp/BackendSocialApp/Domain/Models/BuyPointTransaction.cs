using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Domain.Models
{
    public class BuyPointTransaction
    {
        public Guid Id { get; set; }

        public ConsumerUser User { get; set; }

        public DateTime SubmitDateUtc { get; set; }

        public Point Point { get; set; }

        public int PointValue { get; set; }

        public string ProductId { get; set; }

        public string TransactionId { get; set; }

        public string TransactionJson { get; set; }
    }
}
