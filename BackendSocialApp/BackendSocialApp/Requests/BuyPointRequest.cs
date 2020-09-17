using BackendSocialApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Requests
{
    public class BuyPointRequest
    {
        public string TransactionJson { get; set; }

        public string TransactionId { get; set; }

        public string ProductId { get; set; }

        public PointType PointType { get; set; }

    }
}
