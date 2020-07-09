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
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci1";
                user.Email = "falci1@falci.com";
                user.FullName = "Falcı User 1";
                user.PicturePath = "cheetah.jpg";
                user.Status = UserStatus.Active;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleFalci).Wait();
                }
            }

            if (userManager.FindByNameAsync("falci2").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci2";
                user.Email = "falci2@falci.com";
                user.FullName = "Falcı User 2";
                user.PicturePath = "eagle.jpg";
                user.Status = UserStatus.Active;
                user.ConnectionStatus = ConnectionStatus.Online;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleFalci).Wait();
                }
            }

            if (userManager.FindByNameAsync("falci3").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci3";
                user.Email = "falci3@falci.com";
                user.FullName = "Falcı User 3";
                user.PicturePath = "duck.jpg";
                user.Status = UserStatus.Active;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleFalci).Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user1";
                user.Email = "user1@yahoo.com";
                user.FullName = "User 1";
                user.Status = UserStatus.Active;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleConsumer).Wait();
                }
            }

            if (userManager.FindByNameAsync("user2").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user2";
                user.Email = "user2@yahoo.com";
                user.FullName = "User 2";
                user.Status = UserStatus.Active;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleConsumer).Wait();
                }
            }

            if (userManager.FindByNameAsync("user3").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user3";
                user.Email = "user3@yahoo.com";
                user.FullName = "User 3";
                user.Status = UserStatus.Active;

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Constants.RoleConsumer).Wait();
                }
            }
        }

        public static void SeedNews(AppDbContext context)
        {
            var news1 = context.NewsList.FirstOrDefault(a => a.Id.ToString() == "52D6745B-CF69-4648-8DF5-E3C6D7470221");
            if (news1 == null)
            {
                context.MainFeeds.Add(new News
                {
                    Id = new Guid("52D6745B-CF69-4648-8DF5-E3C6D7470221"),
                    Title = "Başlık 1",
                    InfoHtml = "Info 1",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    DetailedInfo = "Detail Information 1"
                });
            }

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

            survey = context.Surveys.FirstOrDefault(a => a.Id.ToString() == "BC3301F8-B6B2-4D40-AB8B-C22033652408");
            if (survey == null)
            {
                survey = new Survey
                {
                    Id = new Guid("BC3301F8-B6B2-4D40-AB8B-C22033652408"),
                    Title = "Survey 2 - Personality",
                    InfoHtml = "Info 1",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    SurveyType = SurveyType.Personality
                };

                context.MainFeeds.Add(survey);

                var surveyItem = new SurveyItem
                {
                    Question = "Survey 2 - Question 1",
                    Order = 1,
                    Survey = survey,
                    MaxSelectableAnswerNumber = 1,
                    QuestionWeight = 10
                };

                context.SurveyItems.Add(surveyItem);

                var answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Answer 1",
                    AnswerWeight = 1,
                    Order = 1,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Answer 2",
                    AnswerWeight = 5,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Answer 3",
                    AnswerWeight = 10,
                    Order = 3,
                };

                context.SurveyItemAnswers.Add(answer);

                ///////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Survey 2 - Question 2",
                    Order = 2,
                    Survey = survey
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Q2 - Answer 1",
                    AnswerWeight = 5,
                    Order = 1,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Q2 - Answer 2",
                    AnswerWeight = 2,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Q2 - Answer 3",
                    AnswerWeight = 10,
                    Order = 3,
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 5,
                    ResultInformation = "Result 1"
                });

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 10,
                    ResultInformation = "Result 2"
                });

                context.SurveyResultItems.Add(new SurveyResultItem
                {
                    Survey = survey,
                    Point = 15,
                    ResultInformation = "Result 3"
                });

            }

            var news2 = context.NewsList.FirstOrDefault(a => a.Id.ToString() == "DFCED86B-C971-4E35-AA5F-C592A460D0D7");
            if (news2 == null)
            {
                news2 = new News
                {
                    Id = new Guid("DFCED86B-C971-4E35-AA5F-C592A460D0D7"),
                    Title = "ListNews 1",
                    InfoHtml = "Info 1",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active
                };

                context.MainFeeds.Add(news2);

                var surveyItem = new NewsItem
                {
                    News = news2,
                    Order = 1,
                    Information = "1 Information Detail"
                };

                context.NewsItems.Add(surveyItem);

                surveyItem = new NewsItem
                {
                    News = news2,
                    Order = 2,
                    Information = "2 Information Detail"
                };

                context.NewsItems.Add(surveyItem);

                surveyItem = new NewsItem
                {
                    News = news2,
                    Order = 3,
                    Information = "3 Information Detail"
                };

                context.NewsItems.Add(surveyItem);
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
