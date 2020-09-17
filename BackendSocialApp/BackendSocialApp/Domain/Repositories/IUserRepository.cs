using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Paging;
//using BackendSocialApp.Domain.Queries;

namespace BackendSocialApp.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Point> GetPointAsync(string productId, PointType pointType);

        Task SaveBuyPointTransaction(BuyPointTransaction transaction);

        void UpdateUser(ConsumerUser user);

        Task<List<Point>> GetPoints(PointType pointType);
    }
}
