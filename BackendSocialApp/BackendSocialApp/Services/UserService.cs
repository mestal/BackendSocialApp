using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;

namespace BackendSocialApp.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        public IUnitOfWork _unitOfWork;

        public UserService(UserManager<ApplicationUser> userManager, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> BuyPoint(ConsumerUser user, string transactionJson, string transactionId, string productId, PointType pointType)
        {
            var point = await _userRepository.GetPointAsync(productId, pointType);
            if(point == null)
            {
                throw new BusinessException("PointNotFound", "Puan bulunamadı.");
            }

            var newTransaction = new BuyPointTransaction()
            {
                Point = point,
                PointValue = point.PointValue,
                ProductId = productId,
                SubmitDateUtc = DateTime.UtcNow,
                TransactionId = transactionId,
                TransactionJson = transactionJson,
                User = user
            };

            await _userRepository.SaveBuyPointTransaction(newTransaction);

            user.Point = user.Point + point.PointValue;

            _userRepository.UpdateUser(user);

            await _unitOfWork.CompleteAsync();

            return user.Point;

        }

        public async Task<List<Point>> GetPoints(PointType pointType)
        {
            return await _userRepository.GetPoints(pointType);
        }
    }
}
