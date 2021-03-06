﻿using BackendSocialApp.Domain.Models;
using BackendSocialApp.Domain.Repositories;
using BackendSocialApp.Paging;
using BackendSocialApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendSocialApp.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context) { }

        public Task<Point> GetPointAsync(string productId, PointType pointType)
        {
            return Task.FromResult(
                _context.Points
                    .Where(c => c.ProductId == productId && c.PointType == pointType)
                    .FirstOrDefault()
            );
        }

        public Task SaveBuyPointTransaction(BuyPointTransaction transaction)
        {
            return _context.BuyPointTransactions.AddAsync(transaction);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _context.ApplicationUsers.Update(user);
        }

        public Task<List<Point>> GetPoints(PointType pointType)
        {
            return Task.FromResult(
                _context.Points
                    .Where(c => c.PointType == pointType).OrderBy(a => a.Order).ToList()
            );
        }

        public Task<FortuneTellerUser> GetFortuneTellerUser(Guid userId)
        {
            return Task.FromResult(
                _context.FortuneTellerUsers.Include(a => a.FalTypes)
                    .Where(c => c.Id == userId).FirstOrDefault()
            );
        }
    }
}
