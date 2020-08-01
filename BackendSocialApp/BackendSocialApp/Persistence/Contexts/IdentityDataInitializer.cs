using BackendSocialApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Persistence.Contexts
{
    public class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, AppDbContext context)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedNews(context);
            SeedFortuneTellings(context);
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            var result = roleManager.RoleExistsAsync(Constants.RoleAdmin).Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Constants.RoleAdmin;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            result = roleManager.RoleExistsAsync(Constants.RoleFalci).Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Constants.RoleFalci;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            result = roleManager.RoleExistsAsync(Constants.RoleConsumer).Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = Constants.RoleConsumer;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser user = new AdminUser();
                user.UserName = "admin";
                user.Email = "admin@falci.com";
                user.FullName = "Admin User";
                user.Status = UserStatus.Active;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleAdmin).Wait();
                }
            }

            if (userManager.FindByNameAsync("falci1").Result == null)
            {
                FortuneTellerUser user = new FortuneTellerUser();
                user.UserName = "falci1";
                user.Email = "falci1@falcim.xyz";
                user.FullName = "Falcı User 1";
                user.PicturePath = "cheetah.jpg";
                user.Status = UserStatus.Active;
                user.EmailConfirmed = true;
                user.CoffeFortuneTellingCount = 5;
                user.CoffeePointPrice = 100;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleFalci).Wait();
                }
            }

            if (userManager.FindByNameAsync("falci2").Result == null)
            {
                FortuneTellerUser user = new FortuneTellerUser();
                user.UserName = "falci2";
                user.Email = "falci2@falci.com";
                user.FullName = "Falcı User 2";
                user.PicturePath = "eagle.jpg";
                user.Status = UserStatus.Active;
                user.ConnectionStatus = ConnectionStatus.Online;
                user.EmailConfirmed = true;
                user.CoffeFortuneTellingCount = 8;
                user.CoffeePointPrice = 125;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleFalci).Wait();
                }
            }

            if (userManager.FindByNameAsync("falci3").Result == null)
            {
                FortuneTellerUser user = new FortuneTellerUser();
                user.UserName = "falci3";
                user.Email = "falci3@falci.com";
                user.FullName = "Falcı User 3";
                user.PicturePath = "duck.jpg";
                user.Status = UserStatus.Active;
                user.EmailConfirmed = true;
                user.CoffeFortuneTellingCount = 16;
                user.CoffeePointPrice = 140;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleFalci).Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                ConsumerUser user = new ConsumerUser();
                user.UserName = "user1";
                user.Email = "user1@yahoo.com";
                user.FullName = "User 1";
                user.Status = UserStatus.Active;
                user.EmailConfirmed = true;
                user.Point = 1000000;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleConsumer).Wait();
                }
            }

            if (userManager.FindByNameAsync("user2").Result == null)
            {
                ConsumerUser user = new ConsumerUser();
                user.UserName = "user2";
                user.Email = "user2@yahoo.com";
                user.FullName = "User 2";
                user.Status = UserStatus.Active;
                user.EmailConfirmed = true;
                user.Point = 1000000;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleConsumer).Wait();
                }
            }

            if (userManager.FindByNameAsync("user3").Result == null)
            {
                ConsumerUser user = new ConsumerUser();
                user.UserName = "user3";
                user.Email = "user3@yahoo.com";
                user.FullName = "User 3";
                user.Status = UserStatus.Active;
                user.EmailConfirmed = true;
                user.Point = 1000000;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleConsumer).Wait();
                }
            }
        }

        public static void SeedNews(AppDbContext context)
        {
            var survey = context.Surveys.FirstOrDefault(a => a.Id.ToString() == "49CA55E2-501A-4A8A-8984-ED100C19BFBF");
            if (survey == null)
            {
                survey = new Survey
                {
                    Id = new Guid("49CA55E2-501A-4A8A-8984-ED100C19BFBF"),
                    Title = "Güneş Falına Göre Evleneceğin Kişinin Adını Söylüyoruz!",
                    InfoHtml = "Güneş falına göre evleneceğin kişinin adını söylüyoruz! Merak ediyorsan buyur teste!",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    SurveyType = SurveyType.Personality,
                    MainPhoto = "s-9289d0bb82dea831dff7935da8e60121c650d1b0.jpg"
                };

                context.MainFeeds.Add(survey);

                var surveyItem = new SurveyItem
                {
                    Question = "Öncelikle, hangi cinsiyetten HOŞLANDIĞINI öğrenelim?",
                    Order = 1,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                var answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-5f8348536b254353eaaa2eb8fb5a7a4f4d5b54b4.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 2,
                    PicturePath = "s-59e725075d0982f80ddff42e517d4c5242573cf3.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 3,
                    PicturePath = "s-8fe552e63cca4131fde95392f38edf759274ffe3.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                ///////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Burcun ne peki?",
                    Order = 2,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Koç",
                    AnswerWeight = 1,
                    Order = 1,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Boğa",
                    AnswerWeight = 0,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "İkizler",
                    AnswerWeight = 0,
                    Order = 3,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Yengeç",
                    AnswerWeight = 0,
                    Order = 4,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Aslan",
                    AnswerWeight = 0,
                    Order = 5,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Başak",
                    AnswerWeight = 0,
                    Order = 6,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Terazi",
                    AnswerWeight = 0,
                    Order = 7,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Akrep",
                    AnswerWeight = 0,
                    Order = 8,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Yay",
                    AnswerWeight = 0,
                    Order = 9,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Oğlak",
                    AnswerWeight = 0,
                    Order = 10,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Kova",
                    AnswerWeight = 0,
                    Order = 11,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Balık",
                    AnswerWeight = 0,
                    Order = 12,
                };

                context.SurveyItemAnswers.Add(answer);

                ///////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Doğduğun yılın son rakamını sorsak?",
                    Order = 3,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "0",
                    AnswerWeight = 1,
                    Order = 1,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "1",
                    AnswerWeight = 0,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "2",
                    AnswerWeight = 0,
                    Order = 3,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "3",
                    AnswerWeight = 0,
                    Order = 4,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "4",
                    AnswerWeight = 0,
                    Order = 5,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "5",
                    AnswerWeight = 0,
                    Order = 6,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "6",
                    AnswerWeight = 0,
                    Order = 7,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "7",
                    AnswerWeight = 0,
                    Order = 8,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "8",
                    AnswerWeight = 0,
                    Order = 9,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "9",
                    AnswerWeight = 0,
                    Order = 10,
                };

                context.SurveyItemAnswers.Add(answer);

                ///////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Doğduğun gün tek mi yoksa çift sayı mı?",
                    Order = 4,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Tek",
                    AnswerWeight = 1,
                    Order = 1,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Çift",
                    AnswerWeight = 0,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////
                
                surveyItem = new SurveyItem
                {
                    Question = "Haftanın hangi günü doğdun?",
                    Order =5,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Pazartesi",
                    AnswerWeight = 1,
                    Order = 1,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Salı",
                    AnswerWeight = 0,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Çarşamba",
                    AnswerWeight = 0,
                    Order = 3,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Perşembe",
                    AnswerWeight = 0,
                    Order = 4,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Cuma",
                    AnswerWeight = 0,
                    Order = 5,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Cumartesi",
                    AnswerWeight = 0,
                    Order = 6,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Pazar",
                    AnswerWeight = 0,
                    Order = 7,
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Bu fotoğraflardan birini seçmeni istesek?",
                    Order = 6,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-8259e9939e5749a7756d0ec287b37f2ea94f2b3f.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 2,
                    PicturePath = "s-51eeb8da6e6b278f552e75180766afd664ce2a4f.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 3,
                    PicturePath = "s-11f9fafbe3ab83c9c5d0c1a05864d51f07853a63.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 4,
                    PicturePath = "s-6bae6cecf3c661bb21167b3345eb50cf35d625a5.webp"
                };

                context.SurveyItemAnswers.Add(answer);


                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 5,
                    PicturePath = "s-98cabf817c3fba773b6e3e474e073b2313edfe9d.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 6,
                    PicturePath = "s-3c59febf0c8ca934f055ecf41762903ee55458c8.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Şu anda gözlerini kapat ve güneşi düşün. Hayalinde hangi renk yoğunlukta?",
                    Order = 7,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-36ff5da2830680ca2371ef9d379f791b5dfdaa89.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 2,
                    PicturePath = "s-d651747da5c708961bb13daaefa4002c94d6e6b5.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 3,
                    PicturePath = "s-0c24c116db33e2ab4ca4c67055d5cbbee6014be9.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 0,
                    Order = 4,
                    PicturePath = "s-e3241c5d7ee21410656839d9b6e2b8b94d06a5de.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Peki son olarak, güneşin batışını nerede izlemek isterdin?",
                    Order = 8,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Filyos",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-b0349dab9148bea57941dc874bda1add3c9dbc29.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Kerpe",
                    AnswerWeight = 0,
                    Order = 2,
                    PicturePath = "s-d3f49986255eb860e38dabb7fbfa295b77f8c5a9.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Kapadokya",
                    AnswerWeight = 0,
                    Order = 3,
                    PicturePath = "s-f3a13c87dec0331a3152fd2d89c75c0edd576d57.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Bozcaada",
                    AnswerWeight = 0,
                    Order = 4,
                    PicturePath = "s-687980c63eeb7dd878198dfcc0eeca0441a03eb6.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 8,
                    ResultInformation = "Abdülrezzak"
                });

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 10,
                    ResultInformation = "Zübeyde"
                });

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 15,
                    ResultInformation = "Abdülmuttalip"
                });
            }

            survey = context.Surveys.FirstOrDefault(a => a.Id.ToString() == "1C81B03A-9CD4-4EB6-BF2E-B41EBAB25BC2");
            if (survey == null)
            {
                survey = new Survey
                {
                    Id = new Guid("1C81B03A-9CD4-4EB6-BF2E-B41EBAB25BC2"),
                    Title = "Kuantum Falına Göre Evleneceğin Kişinin Maaşını Söylüyoruz!",
                    InfoHtml = "Evleneceğin kişinin maaşını söylüyoruz! Eğer merak ediyorsan; Cevabını burada bulabilirsin! Haydi teste!  😎",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    SurveyType = SurveyType.Personality,
                    MainPhoto = "s-feba52b2dbafe667ac2fc64f78ee55f96aad069a.jpg"
                };

                context.MainFeeds.Add(survey);

                var surveyItem = new SurveyItem
                {
                    Question = "1. Öncelikle cinsiyetini söyler misin?",
                    Order = 1,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                var answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-83e075d84246ff908a0e7ada9db72ef0332b494e.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-2c8ac6a4789cefc4e2f2c2f336b2b414524442fc.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                ///////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "2. Şimdi de yaşını söyler misin?",
                    Order = 2,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-ad841411bf707221daeb32d156999a1a9dde3c94.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-d461454c2b693c3db84f579147fd80582f2a9c14.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 3,
                    PicturePath = "s-09edfa07fffa2e3755b65d6c002c7632cad6bed8.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 4,
                    PicturePath = "s-53054ee925ff5eb28e41b8d588a80084f8eb38e4.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 5,
                    PicturePath = "s-7be2d9b2bc56a30381518ae2035c9037e2dd1f8e.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 6,
                    PicturePath = "s-3b56b92dee31d2f6cb937888aaf2b1b5130923d8.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "3. İsminin kaç heceden oluştuğunu söyler misin?",
                    Order = 3,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    PicturePath = "s-67636d006cd6cb56a430419c52dc444a1d25d405.webp",
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-669ad6a13d16f6333cc19c12b8fbc89195da75c0.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-be0be5d69117737ab584445d94ee485d845b963b.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 3,
                    PicturePath = "s-e545b334df6b152731b6b4db77c70b342bc8c77a.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 4,
                    PicturePath = "s-251da6183f9624ecdd3128dfb92baa5feda1e28d.webp"
                };

                context.SurveyItemAnswers.Add(answer); 

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 5,
                    PicturePath = "s-f372d2c609ab7e38a972e9ecdbad4dd909e2390e.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 6,
                    PicturePath = "s-e317b66717f9f83275cb6cd4fe10f6a431adf1c0.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "4. Peki isminin ikinci harfi ünlü mü yoksa ünsüz mü?",
                    Order = 4,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    PicturePath = "s-0a531c52de200f3428fa8954ac02251b19d9d55d.webp",
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-019d6a98dbb4b5261aac12abac6154541d685a1e.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-980f8cd596bf62bc11473ce1b462f48dee865389.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "5. Soyadının kaç harften oluştuğunu da söyler misin?",
                    Order = 5,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-be0be5d69117737ab584445d94ee485d845b963b.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-e545b334df6b152731b6b4db77c70b342bc8c77a.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 3,
                    PicturePath = "s-251da6183f9624ecdd3128dfb92baa5feda1e28d.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 4,
                    PicturePath = "s-f372d2c609ab7e38a972e9ecdbad4dd909e2390e.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 5,
                    PicturePath = "s-608b2c6e402b4d23b7f3dee44b0a8e8b5400bf36.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 6,
                    PicturePath = "s-34a7ce7cf123996bfec56b17ea3cb2c6b9952e50.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "6. Doğduğun saat de önem taşıyor. Hangi saat aralığında doğdun?",
                    Order = 6,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-b95692e87f64c729bd499d559784443b966b5a10.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-3d447ebcb7bf99a3eae706ff6d481525b63b9a1a.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 3,
                    PicturePath = "s-4c056399b53046619be75879e807879e5cab0e7b.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 4,
                    PicturePath = "s-8dccf58a6bea104c1a35e2a8d4630c29827994d3.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 5,
                    PicturePath = "s-684895dfb792852f511bad827dd5a1142b33769f.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 6,
                    PicturePath = "s-aa98b50ba35e638694e03eef392b3a081b03879e.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 7,
                    PicturePath = "s-33d274e0cf64c56737d1ec901a74bf341eebce9d.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 8,
                    PicturePath = "s-7c3943bba5eabc67a21d87792680545456c8b50a.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 9,
                    PicturePath = "s-61abfcd718cf279a79a554adacb1c3d2f7859401.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 10,
                    PicturePath = "s-2511c60ad48c368c4bde07202623449c9e87f034.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 11,
                    PicturePath = "s-a7ac14b3737b41926b7b6a64462465efbb24586f.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 12,
                    PicturePath = "s-7560bd71874f188bea8178d75d26f2ca5b05c335.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 13,
                    PicturePath = "s-299db7ce3bbc4dfeef2db18183229abec2e0ccd3.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "7. Annenin isminin son harfi ünlü mü yoksa ünsüz mü?",
                    Order = 7,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-e2fe9b53bcce3ec112e07cf51f61c9da6c441ad4.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-aa1b1c0ce40c00608f7f7cfbdb0720a00e4b2049.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "8. Şimdi de hangi ayda doğduğunu söyle bakalım.",
                    Order = 8,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-61744d4758378ed6a58686b5be737386fa157151.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-d6ca461d68cbcb50c64e703e994f80d7f3cd84bc.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 3,
                    PicturePath = "s-b06cce604ac9a480987780083bc82435b332ee88.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 4,
                    PicturePath = "s-3de53c52cb3bb3e43c1d02625a0422b2e7b1944f.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 5,
                    PicturePath = "s-ea59a22c18b8ba0f720cf741a452076975611a96.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 6,
                    PicturePath = "s-ae12baf0711cc35b006ebb8b6de07125aa8aa602.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 7,
                    PicturePath = "s-a4c023a53037e758e26cde8860fdfd53d1abc303.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 8,
                    PicturePath = "s-2d919bf30e8d24d37954376454e855be91933843.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 9,
                    PicturePath = "s-694cde46100e2d5c92a8bea79f6e00a33e899665.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 10,
                    PicturePath = "s-297485aafb7858348ab4e7f5fab57af1e8390c77.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 11,
                    PicturePath = "s-28a191131a9b9a340f7e4dfa630d390ed6bca520.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 12,
                    PicturePath = "s-67954d52cce3e178670de9f7197a6499e8a2779b.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "9. Son olarak, doğduğun yılın son iki hanesini topladığında çıkan sonuç tek mi yoksa çift mi? ( 9+4 = 13 = Tek )",
                    Order = 9,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    VideoPath = "s-91ee2a24b7bff8b499b1b589bf5c8d0b62478b81.webm",
                    QuestionWeight = 1
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 1,
                    PicturePath = "s-1a438891c0ac68a8dc5228dcfb0afa784bfd7152.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "",
                    AnswerWeight = 1,
                    Order = 2,
                    PicturePath = "s-c256f1520f89425e5279b95b6d10698e6891e71b.webp"
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////


                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 8,
                    ResultInformation = "4000"
                });

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 10,
                    ResultInformation = "7000"
                });

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 15,
                    ResultInformation = "10000"
                });
            }

            var news1 = context.NewsList.FirstOrDefault(a => a.Id.ToString() == "4D9BBCAE-579B-415B-8E92-8BAAB77A6876");
            if (news1 == null)
            {
                news1 = new News
                {
                    Id = new Guid("4D9BBCAE-579B-415B-8E92-8BAAB77A6876"),
                    Title = "Eğer Kahve Falınızda Bu 17 Sembolden Birini Görüyorsanız, Kesinlikle Dikkat Etmelisiniz!",
                    InfoHtml = "Kahve falında gördüğünüz sembollerin ne anlama geldiğini bilmiyor musunuz? Eğer bahsettiğimiz bu semboller falınızda varsa kesinlikle dikkat etmelisiniz.",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    MainPhoto = "s-9cc45623c9324070764192652243d9eade288291.jpg"
                };

                context.MainFeeds.Add(news1);

                var newsItem = new NewsItem
                {
                    Title = "1. Yılan",
                    Information = "Kahve falınızda eğer yılan görüyorsanız, bu pek iyiye işaret değildir. Etrafınızda birtakım düşmanlarınızın olduğuna işaret eder. Yakın zamanda hayatınızda bir karışıklık olacaktır. Yakın arkadaşlarınıza ve akrabalarınıza dikkat etmelisiniz. Çünkü ne geliyorsa yakınınızdan geliyor. O yüzden temkinli olmanızda ve etrafınızdaki kişilere çok güvenmemenizde fayda vardır.",
                    Order = 1,
                    VideoPath = "s-c82173a3d7e8b35cac934a3a5471de6d9e21aeec.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "2. Ray",
                    Information = "Aman dikkat, eğer falınızda raya benzer bir sembol görüyorsanız sağlığınıza dikkat etmelisiniz. Çünkü yakın zamanda yorgun düşebilir, sağlığınızla ilgili problemler yaşayabilirsiniz. Bundan dolayı hem beslenmenize hem de uyku düzeninize önem vermelisiniz. Aksi takdirde bir hastalığa kapılabilirsiniz. Bizden söylemesi.",
                    Order = 2,
                    VideoPath = "s-48996164c8f1dc3b10f0dbdbede68dd36ca2076b.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "3. Kedi",
                    Information = "Eğer ki falınızda kedi görüyorsanız bu sizin yakınınızdaki insanlara dikkat etmeniz gerektiğini gösterir. Çünkü fincanda beliren kedi sembolü, sizin etrafınızdaki kişilerden bir zarar göreceğinize işaret eder. Sırlarınızı arkadaşlarınıza veya akrabalarınıza anlatmamaya dikkat edin. Çünkü sizi nasıl bir tehlikenin beklediğini bilemezsiniz.",
                    Order = 3,
                    VideoPath = "s-a137193e8b3393a80eec886adedc08355845229f.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "4. Geyik",
                    Information = "Açıkçası kahve falınızda geyik sembolü görüyorsanız, bu kötü bir haber alacağınıza işaret eder. Bu ya birdenbire oluşacak bir hastalık ya da cebinizden gidecek bir para olabilir. Etrafınızdaki kişilerin sağlık problemleri ile karşılaşabilirsiniz. Ya da yatırım yaptığınız bir işin altından kayıplarla çıkabilirsiniz.",
                    Order = 4,
                    VideoPath = "s-640dd2e61bba80033c91c95034e4b7c91e104b43.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "5. Ahtapot",
                    Information = "Kahve falınızda ahtapot görmek, sizin için bir uyarı niteliği taşır. Herhangi bir iş için tehlike altında olduğunuza işarettir. Ve bu tehlike sizi yıpratacak derecede de olabilir. Eğer birine çok fazla güveniyorsanız, bir girişimde bulunduysanız dikkat etmelisiniz. Yakın birinin sizin kötülüğünüzü istemesi ve üzerinizde de bunun gölgesini hissetmeniz muhtemeldir. O yüzden bu uyarıyı dikkate almalısınız. Bizden söylemesi.",
                    Order = 5,
                    VideoPath = "s-cde78cc71ae52d07750e3c08c30337e488c1c98f.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "6. Ağ",
                    Information = "Eğer kahve falınızda bir ağ görüyorsanız yakın çevrenize dikkat etmelisiniz. Çünkü etrafınızdaki birtakım insanlar sizin başarısız olmanızı isteyecektir. Ve bunun için de elinden geleni yapacaktır. O yüzden attığınız her adıma dikkat etmeli, sizi kandırmaya çalışan bu insanlara karşı tedbirli olmalısınız.",
                    Order = 6,
                    VideoPath = "s-0b01f842b6ade922509bd04d77eec67028c01040.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
                newsItem = new NewsItem
                {
                    Title = "7. Akrep",
                    Information = "Herkesin korkutucu bulduğu akrep, kahve falında da kötü bir anlam ifade ediyor. Eğer ki falınızda akrep sembolünü görüyorsanız, etrafınızdaki insanlardan kendinizi korumalısınız. Çünkü çok büyük bir tehlikenin eşiğinde olabilirsiniz. Başınıza kötü bir şeyin gelmesi an meselesidir. O yüzden her şeye ve herkese karşı dikkatli olmalısınız.",
                    Order = 7,
                    VideoPath = "s-3964e2fafc24ca3a3ca3e3e6217beb7d9cfbd22c.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "8. Baykuş",
                    Information = "Baykuş sembolü, senin yakın arkadaşlarının negatif olduğuna ve hayatına da olumsuz etki ettiğine işaret eder. Eğer böyle bir arkadaşınız varsa size müdahale etmesine izin vermemeli ve kararlarınızda etkili olmamalıdır. Bir an önce bu arkadaşlıklardan kurtulmanız gerekmektedir. Aksi takdirde hayatınızda sürekli sorun çıkaran bir arkadaşınız olacaktır. Ve bu da sizi epey zorlayacaktır.",
                    Order = 8,
                    VideoPath = "s-8d0a64257984ac5fae489666fa05594abc677361.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
                newsItem = new NewsItem
                {
                    Title = "9. Çorap",
                    Information = "Genellikle kahve falınızda çorap görmek, sizin mal varlığınızın büyük bir tehlike altında olduğuna işaret eder. Bu durumda mal varlığınız ile ilgili konuları takip etmeli ve gereksiz harcamalarda bulunmamalısınız. Aksi takdirde büyük bir kayıp yaşayabilirsiniz.",
                    Order = 9,
                    VideoPath = "s-c957b0369cf85241f06d99922c99496b30cc2ed4.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
                newsItem = new NewsItem
                {
                    Title = "10. Cetvel",
                    Information = "Eğer kahve falınızda cetvel görüyorsanız iş hayatınızda çeşitli problem yaşayacağınızı söylememiz gerekir. İşinize gereken özeni göstermeli ve sizi aşağıya çekmek isteyenlere karşı dikkatli olmalısınız. Aksi takdirde işinizle alakalı ciddi problemler sizi bekliyor olabilir.",
                    Order = 10,
                    VideoPath = "s-632e084ca0c48cb821fa5710dd875a7707c67086.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "11. İskelet",
                    Information = "Eğer kahve falınızda iskelet öylece görünüyorsa, kesinlikle dikkatli olmanız gerekir. Çünkü iskelet görmek, sizin büyük bir olayla karşı karşıya kalacağınızı ve bunun sonucunda da fazlasıyla üzüleceğinizi simgeler. Bu uzaktan gelen bir haber de ya da çevrenizdekilerin sizi üzeceği bir durum da olabilir. Ama sonucunda her şekilde sizi üzecek bir durum söz konusudur.",
                    Order = 11,
                    VideoPath = "s-63f6c31df566bb8a69d580b402de043dca9e6bad.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "12. Kazan",
                    Information = "Belki de görüp görebileceğiniz en kötü sembol olabilir. Kazan sembolü yakın zamanda bir ölüm haberi alacağınızı ve bu haberle birlikte yıkılacağınızı temsil eder. O yüzden kazan sembolünü görürseniz kesinlikle bu duruma kendinizi hazırlamalısınız.",
                    Order = 12,
                    VideoPath = "s-50b83a8a1e108e8d360438e1e59c60336b3266e4.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
                newsItem = new NewsItem
                {
                    Title = "13. Tilki",
                    Information = "Eğer kahve falınızda tilki sembolü gördüyseniz kesinlikle dostlarınızı gözden geçirmelisiniz. Çünkü etrafınızda sizin yüzünüze gülen fakat arkanızdan kuyunuzu kazan bir dostunuzun olduğunu söylemeliyiz. O yüzden sizin iyi niyetinizi suistimal eden insanlardan uzak durmalısınız.",
                    Order = 13,
                    VideoPath = "s-ef9619f388f5f415f31e19ec21f1364b754bc08b.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "14. Ustura",
                    Information = "Kahve falınızda ustura görmek, çevrenizdeki insanlar tarafından büyük bir haksızlığa uğrayacağınızı işaret eder. O yüzden yaptığınız her işi, büyük bir titizlikle yapmalısınız. Aksi takdirde, yapmadığınız bir şeyin sizin üstünüze kalması an meselesidir.",
                    Order = 14,
                    VideoPath = "s-da664afdcd717896a7995ced49435169cfe7f055.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
                newsItem = new NewsItem
                {
                    Title = "15. Zürafa",
                    Information = "Kahve falınızda eğer zürafa görüyorsanız ve bir ilişkiniz varsa dikkat etmelisiniz. Bu sembol, önümüzdeki günlerde bu ilişkinin bozulacağına ve ayrılığa kadar gideceğine işarettir. O yüzden ilişkinizi tekrar gözden geçirmeli ve aranızdaki sorunlar çok fazla birikmeden halletmelisiniz.",
                    Order = 15,
                    VideoPath = "s-ce40fc87ef10397dfdcc64ba67b4bd31da9e7291.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
                newsItem = new NewsItem
                {
                    Title = "16. Uçurtma",
                    Information = "Eğer kahve falınızda uçurtma görüyorsanız, etrafınızdaki kişiler sizin hayallerinize ulaşmakta engel çıkaracaktır. Sizi bir şekilde istediğiniz şeylerden uzaklaştırmak için ellerinden geleni yapmaya çalışacaklardır. O yüzden kafanızdaki planları herkesle paylaşmadan önce düşünmelisiniz.",
                    Order = 16,
                    VideoPath = "s-486b7d16c80dddf6615db386b09d51428eeee6a1.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "17. Timsah",
                    Information = "Kendinizi hazırlasanız iyi edersiniz, çünkü kahve falında timsah görmek kötü bir haberi simgeler. Alacağınız kötü bir haberle üzüleceğinize ve sizi uzun bir süre yıpratacağı anlamına gelir. O yüzden kendinizi bu duruma alıştırmalısınız. Ani tepkiler vermemelisiniz.",
                    Order = 17,
                    VideoPath = "s-7323d0e6cd83a38b199552a99bc74788ce452a30.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
            }

            news1 = context.NewsList.FirstOrDefault(a => a.Id.ToString() == "B5DC7A71-ECCF-4751-A8FC-69FF63FB930F");
            if (news1 == null)
            {
                news1 = new News
                {
                    Id = new Guid("B5DC7A71-ECCF-4751-A8FC-69FF63FB930F"),
                    Title = "Bir Falcı ile IV. Murat Arasında Yaşanan Bu İlginç Olayı Mutlaka Okumalısınız!",
                    InfoHtml = "Ekşi Sözlük yazarlarından the fat of the land 'in öğrenildiğinde ufku iki katına çıkaran şeyler başlığında ele aldığı ve 4. Murat zamanında yapılan Yenikapı'nın hikayesinin anlatıldığı bu entry hepinizi çok şaşırtacak!"
                        + "Not: Bu yazı, yazarının izniyle yayımlanmaktadır!"
                        + "Kaynak: https://eksisozluk.com/entry/45168754",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    MainPhoto = "s-92754f3e4bc97ebe998ac3c2d639a60681f180f6.jpg"
                };

                context.MainFeeds.Add(news1);

                var newsItem = new NewsItem
                {
                    Title = "4. Murat Devri'nde padişah tarafından, mey (şarap), afyon ve fal bakmak yasaklanmış. İstanbul’da bütün meyhaneler ve keshaneler “gizli” takılmaya başlamış.",
                    Order = 1,
                    PicturePath = "s-a00debe38b4c98c7fc5056d8abd8b143b0ed3d31.webp",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "4. Murat bir gece, tebdil-i kıyafet İstanbul’a inmeye karar vermiş ve bir sandala binmiş.",
                    Information = "Sandalcı müşterisinin sultan olduğunu bilmiyormuş tabii. Bir ara, sandalın yanından sarkan bir ipi çekmiş. İpin ucunda bir testi! Sultan, “ne var o testinin içinde?” diye sormuş. Sandalcı “ne olacak, mey işte” diye gülerek müşterisine ikram etmiş. Her ne kadar yasaklamış olsa da, 4. Murat’ın alkolle arasının iyi olduğu bilinir. İkramı kabul etmiş ama yine de, “mey yasak. Hünkarımız görse kafanı vurdurtur. Bundan korkmuyor musun?” diye sormaktan da geri kalmamış. Sandalcı da haliyle, “Yahu hünkar nereden görecek bizi denizin ortasında?” demiş.",
                    Order = 2,
                    PicturePath = "s-6da57d6e3b6030f3af9f715554f160fd114810fc.webp",
                    News = news1
                };

                newsItem = new NewsItem
                {
                    Title = "Aradan biraz zaman geçmiş. Sandalcı bu kez de teknenin tahtalarından birini kaldırıp aradan afyon çıkarmış ve nargilesine atarak körüklemeye başlamış.",
                    Information = "Gönlü zengin adam, hemen müşterisine de ikram etmiş. Sultan yine kabul etmiş ama yasağı gene hatırlatmış. Sandalcı aynı şekilde, “Kim görecek ki bizi denizin ortasında?” demiş.",
                    Order = 3,
                    VideoPath = "s-c7010af6e250d4a84c837fbf97d35bdf20053f54.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "Biraz daha vakit geçmiş. Bizim sandalcı cebinden fal taşlarını çıkarmış. Hünkara, “Ver 5 akçe de falına bakayım.” demiş.",
                    Information = "Fal 4. Murat’ın en kızdığı şeymiş, ama “hadi biraz daha sabredeyim.” diye düşünüp, “Bak bari.” demiş.",
                    Order = 4,
                    VideoPath = "s-2a25c5633c74df1a57f74073e9571f6710a6b34e.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "Fal taşlarını elinde çalkalayıp atan sandalcı, “Efendi, sorunu sor bakalım.” demiş. 4. Murat, “Hünkar şu anda nerededir?” diye sormuş.",
                    Information = "Sandalcı taşlara bakıp “Hünkar şu an denizdedir.” demiş. 4. Murat güya endişelenmiş havalarına girip, “Sakın yakınımızda bi' yerde olmasın?” diye sormuş sandalcıya ve tekrar iyice bakmasını söylemiş. Sandalcı taşlara tekrar bakmış ve birden, 4. Murat’ın ayaklarına kapanıp, “Affet beni hünkarım!” diye yalvarmaya başlamış. Kıyıya dönene kadar da yalvarmaya devam etmiş.",
                    Order = 5,
                    VideoPath = "s-4cddda2e789aade40f3f4bea4e4354ca78e3bd38.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "Padişah dayanamayıp, “Sana bi' soru soracağım. Eğer bilirsen seni affederim. Bilemezsen boynunu anında vurduracam” demiş. Sandalcı sevinçle, “Padişahım çok yaşa!” demiş ve merakla soruyu beklemeye başlamış.",
                    Information = "4. Murat, sandalcıya, “Dönüşte istanbul’a hangi kapıdan gireceğim?” diye sormuş. Tabii sandalcı hemen itiraz etmiş, “Hünkarım, şimdi ben hangi kapıyı söylesem, siz başka kapıdan girersiniz. Affınıza sığınarak, gireceğiniz kapıyı bi' kağıda yazsam ve size versem; kapıdan geçtikten sonra okusanız olur mu?” demiş. Hünkar başını “olur” anlamında sallayınca, sandalcı tahminini yazıp kağıdı vermiş.",
                    Order = 6,
                    PicturePath = "s-8cbcda3808dcab3320b38f67b08fb6ff8169aad2.webp",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
                newsItem = new NewsItem
                {
                    Title = "4. Murat kağıdı alır almaz, daha bakmadan, yanındaki fedaisine, “Hemen boynunu vur şu kafirin!” emrini vermiş. Sonra da, “Surlara yeni bir kapı açıla! İstanbul’a oradan gireceğim.” demiş çevresindekilere.",
                    Information = "Kapı 5-10 dakikada açılıp, padişah ve erkanı şehre girmiş. 4. Murat bir ara, sandalcının kağıda hangi kapıyı yazdığını merak etmiş. Kendinden çok eminmiş, laf olsun diye cebindeki kağıda bakmış. Ama okuyunca hayretler içinde kalmış. Sandalcı kağıda şunları yazmış: “Hünkarım, yeni kapınız vatana millete hayırlı uğurlu olsun.”",
                    Order = 7,
                    VideoPath = "s-5bd7970b6c59dcbbc004e54a6c29544b7a7ff58c.webm",
                    News = news1
                };

                context.NewsItems.Add(newsItem);

                newsItem = new NewsItem
                {
                    Title = "O gün bugündür de işte o kapı, “Yenikapı” olarak anılıyormuş.",
                    Order = 8,
                    PicturePath = "s-957bfb52289fe05ab590f5ca70e2759776055bf3.webp",
                    News = news1
                };

                context.NewsItems.Add(newsItem);
            }

            context.SaveChanges();
        }

        public static void SeedFortuneTellings(AppDbContext context)
        {
            if(context.CoffeeFortuneTellings.Count() > 0)
            {
                return;
            }

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 1 ",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 2",
                ReadDateUtc = DateTime.UtcNow.AddDays(-3),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-7),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-5),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 10,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 3",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 4",
                ReadDateUtc = null,
                Status = CoffeeFortuneTellingStatus.SubmittedByUser,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = null,
                Type = CoffeeFortuneTellingType.Money,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 5",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.Love,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 6",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.Business,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 7",
                ReadDateUtc = null,
                Status = CoffeeFortuneTellingStatus.Draft,
                SubmitDateUtc = null,
                SubmitByFortuneTellerDateUtc = null,
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 8",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 9",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 10",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 11",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 12",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.CoffeeFortuneTellings.Add(new CoffeeFortuneTelling
            {
                Id = Guid.NewGuid(),
                Point = 5,
                FortuneTeller = context.FortuneTellerUsers.First(),
                FortuneTellerComment = "Deneme 13",
                ReadDateUtc = DateTime.UtcNow.AddDays(-1),
                Status = CoffeeFortuneTellingStatus.SubmittedByFortuneTeller,
                SubmitDateUtc = DateTime.UtcNow.AddDays(-5),
                SubmitByFortuneTellerDateUtc = DateTime.UtcNow.AddDays(-3),
                Type = CoffeeFortuneTellingType.General,
                User = context.ConsumerUsers.First()
            });

            context.SaveChanges();
        }
    }
}
