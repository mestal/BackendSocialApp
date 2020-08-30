﻿// <auto-generated />
using System;
using BackendSocialApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendSocialApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200829174904_falci description")]
    partial class falcidescription
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackendSocialApp.Domain.Models.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int>("ConnectionStatus");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Gender");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PicturePath");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.CoffeeFortuneTelling", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("FortuneTellerComment");

                    b.Property<Guid?>("FortuneTellerId");

                    b.Property<int>("Point");

                    b.Property<DateTime?>("ReadDateUtc");

                    b.Property<int>("Status");

                    b.Property<DateTime?>("SubmitByFortuneTellerDateUtc");

                    b.Property<DateTime?>("SubmitDateUtc");

                    b.Property<int>("Type");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FortuneTellerId");

                    b.HasIndex("UserId");

                    b.ToTable("CoffeeFortuneTellings");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.CoffeeFortuneTellingPicture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CoffeeFortuneTellingId");

                    b.Property<string>("Path");

                    b.HasKey("Id");

                    b.HasIndex("CoffeeFortuneTellingId");

                    b.ToTable("CoffeeFortuneTellingPictures");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("ParentId");

                    b.Property<Guid>("RefId");

                    b.Property<int>("RefType");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UserComment");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("LikeType");

                    b.Property<Guid>("RefId");

                    b.Property<int>("RefType");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.MainFeed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentCount");

                    b.Property<string>("DetailedInfo");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("DislikeCount");

                    b.Property<string>("InfoHtml");

                    b.Property<int>("LikeCount");

                    b.Property<string>("MainPhoto");

                    b.Property<DateTime>("PublishedDateUtc");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("MainFeeds");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MainFeed");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.NewsItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Information");

                    b.Property<Guid?>("NewsId");

                    b.Property<int>("Order");

                    b.Property<string>("PicturePath");

                    b.Property<string>("Title");

                    b.Property<string>("VideoPath");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsItems");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.SurveyItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaxSelectableAnswerNumber");

                    b.Property<int>("Order");

                    b.Property<string>("PicturePath");

                    b.Property<string>("Question");

                    b.Property<int>("QuestionWeight");

                    b.Property<Guid?>("SurveyId");

                    b.Property<string>("VideoPath");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyItems");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.SurveyItemAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<int>("AnswerWeight");

                    b.Property<int>("Order");

                    b.Property<string>("PicturePath");

                    b.Property<Guid?>("SurveyItemId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyItemId");

                    b.ToTable("SurveyItemAnswers");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.SurveyResultItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PicturePath");

                    b.Property<int>("Point");

                    b.Property<string>("ResultInformation");

                    b.Property<Guid?>("SurveyId");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyResultItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.AdminUser", b =>
                {
                    b.HasBaseType("BackendSocialApp.Domain.Models.ApplicationUser");

                    b.HasDiscriminator().HasValue("AdminUser");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.ConsumerUser", b =>
                {
                    b.HasBaseType("BackendSocialApp.Domain.Models.ApplicationUser");

                    b.Property<DateTime>("BirthTime");

                    b.Property<int>("Point");

                    b.HasDiscriminator().HasValue("ConsumerUser");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.FortuneTellerUser", b =>
                {
                    b.HasBaseType("BackendSocialApp.Domain.Models.ApplicationUser");

                    b.Property<int>("CoffeFortuneTellingCount");

                    b.Property<int>("CoffeePointPrice");

                    b.HasDiscriminator().HasValue("FortuneTellerUser");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.News", b =>
                {
                    b.HasBaseType("BackendSocialApp.Domain.Models.MainFeed");

                    b.HasDiscriminator().HasValue("News");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.Survey", b =>
                {
                    b.HasBaseType("BackendSocialApp.Domain.Models.MainFeed");

                    b.Property<int>("SurveyType");

                    b.HasDiscriminator().HasValue("Survey");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.CoffeeFortuneTelling", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.FortuneTellerUser", "FortuneTeller")
                        .WithMany()
                        .HasForeignKey("FortuneTellerId");

                    b.HasOne("BackendSocialApp.Domain.Models.ConsumerUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.CoffeeFortuneTellingPicture", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.CoffeeFortuneTelling", "CoffeeFortuneTelling")
                        .WithMany("Pictures")
                        .HasForeignKey("CoffeeFortuneTellingId");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.NewsItem", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.News", "News")
                        .WithMany("Items")
                        .HasForeignKey("NewsId");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.SurveyItem", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.Survey", "Survey")
                        .WithMany("Items")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.SurveyItemAnswer", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.SurveyItem", "SurveyItem")
                        .WithMany("Answers")
                        .HasForeignKey("SurveyItemId");
                });

            modelBuilder.Entity("BackendSocialApp.Domain.Models.SurveyResultItem", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.Survey", "Survey")
                        .WithMany("Results")
                        .HasForeignKey("SurveyId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BackendSocialApp.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("BackendSocialApp.Domain.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
