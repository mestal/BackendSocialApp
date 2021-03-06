﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;

namespace BackendSocialApp.Services
{
    public interface IUserService
    {

        Task<int> BuyPoint(ConsumerUser user, string transactionJson, string transactionId, string productId, PointType pointType);

        Task<List<Point>> GetPoints(PointType pointType);

        Task<FortuneTellerUser> GetFortuneTellerUser(Guid userId);
    }
}
