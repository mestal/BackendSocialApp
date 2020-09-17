using BackendSocialApp.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ConsumerUser> ConsumerUsers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<FortuneTellerUser> FortuneTellerUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<CoffeeFortuneTelling> CoffeeFortuneTellings { get; set; }
        public DbSet<CoffeeFortuneTellingPicture> CoffeeFortuneTellingPictures { get; set; }

        public DbSet<MainFeed> MainFeeds { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<SurveyResultItem> SurveyResultItems { get; set; }

        public DbSet<SurveyItem> SurveyItems { get; set; }

        public DbSet<SurveyItemAnswer> SurveyItemAnswers { get; set; }

        public DbSet<NewsItem> NewsItems { get; set; }

        public DbSet<News> NewsList { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Point> Points { get; set; }

        public DbSet<BuyPointTransaction> BuyPointTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
