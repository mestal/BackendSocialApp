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
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            var result = roleManager.RoleExistsAsync("Admin").Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            result = roleManager.RoleExistsAsync("Falci").Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Falci";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            result = roleManager.RoleExistsAsync("Consumer").Result;
            if (!result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = "Consumer";
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

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("falci1").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci1";
                user.Email = "falci1@falci.com";
                user.FullName = "Falcı User 1";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Falci").Wait();
                }
            }

            if (userManager.FindByNameAsync("falci2").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci2";
                user.Email = "falci2@falci.com";
                user.FullName = "Falcı User 2";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Falci").Wait();
                }
            }

            if (userManager.FindByNameAsync("falci3").Result == null)
            {
                ApplicationUser user = new FortuneTellerUser();
                user.UserName = "falci3";
                user.Email = "falci3@falci.com";
                user.FullName = "Falcı User 3";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Falci").Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user1";
                user.Email = "user1@yahoo.com";
                user.FullName = "User 1";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Consumer").Wait();
                }
            }

            if (userManager.FindByNameAsync("user2").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user2";
                user.Email = "user2@yahoo.com";
                user.FullName = "User 2";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Consumer").Wait();
                }
            }

            if (userManager.FindByNameAsync("user3").Result == null)
            {
                ApplicationUser user = new ConsumerUser();
                user.UserName = "user3";
                user.Email = "user3@yahoo.com";
                user.FullName = "User 3";

                IdentityResult result = userManager.CreateAsync(user, "ABcd12%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Consumer").Wait();
                }
            }
        }

        public static void SeedNews(AppDbContext context)
        {
            var news1 = context.ListOfNews.FirstOrDefault(a => a.Id.ToString() == "52D6745B-CF69-4648-8DF5-E3C6D7470221");
            if (news1 == null)
            {
                context.MainFeeds.Add(new News
                {
                    Id = new Guid("52D6745B-CF69-4648-8DF5-E3C6D7470221"),
                    Title = "Başlık 1",
                    Info = "Info 1",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    DetailInformation = "Detail Information 1"
                });
            }

            var survey = context.Surveys.FirstOrDefault(a => a.Id.ToString() == "49CA55E2-501A-4A8A-8984-ED100C19BFBF");
            if (survey == null)
            {
                survey = new Survey
                {
                    Id = new Guid("49CA55E2-501A-4A8A-8984-ED100C19BFBF"),
                    Title = "Survey 1 - TrueFalse",
                    Info = "Info 1",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active,
                    SurveyType = SurveyType.TrueFalse
                };

                context.MainFeeds.Add(survey);

                var surveyItem = new SurveyItem
                {
                    Question = "Survey 1 - Question 1",
                    Order = 1,
                    Survey = survey
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
                    AnswerWeight = 0,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Answer 3",
                    AnswerWeight = 0,
                    Order = 3,
                };

                context.SurveyItemAnswers.Add(answer);

                ///////////////////////////

                surveyItem = new SurveyItem
                {
                    Question = "Survey 1 - Question 2",
                    Order = 2,
                    Survey = survey
                };

                context.SurveyItems.Add(surveyItem);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Q2 - Answer 1",
                    AnswerWeight = 1,
                    Order = 1,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Q2 - Answer 2",
                    AnswerWeight = 0,
                    Order = 2,
                };

                context.SurveyItemAnswers.Add(answer);

                answer = new SurveyItemAnswer
                {
                    SurveyItem = surveyItem,
                    Answer = "Q2 - Answer 3",
                    AnswerWeight = 0,
                    Order = 3,
                };

                context.SurveyItemAnswers.Add(answer);

                /////////////////////////////////////////

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

            survey = context.Surveys.FirstOrDefault(a => a.Id.ToString() == "BC3301F8-B6B2-4D40-AB8B-C22033652408");
            if (survey == null)
            {
                survey = new Survey
                {
                    Id = new Guid("BC3301F8-B6B2-4D40-AB8B-C22033652408"),
                    Title = "Survey 2 - Personality",
                    Info = "Info 1",
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

            var news2 = context.ListOfListNews.FirstOrDefault(a => a.Id.ToString() == "DFCED86B-C971-4E35-AA5F-C592A460D0D7");
            if (news2 == null)
            {
                news2 = new ListNews
                {
                    Id = new Guid("DFCED86B-C971-4E35-AA5F-C592A460D0D7"),
                    Title = "ListNews 1",
                    Info = "Info 1",
                    PublishedDateUtc = DateTime.UtcNow,
                    Status = MainFeedStatus.Active
                };

                context.MainFeeds.Add(news2);

                var surveyItem = new ListNewsItem
                {
                    ListNews = news2,
                    Order = 1,
                    Information = "1 Information Detail"
                };

                context.ListNewsItems.Add(surveyItem);

                surveyItem = new ListNewsItem
                {
                    ListNews = news2,
                    Order = 2,
                    Information = "2 Information Detail"
                };

                context.ListNewsItems.Add(surveyItem);

                surveyItem = new ListNewsItem
                {
                    ListNews = news2,
                    Order = 3,
                    Information = "3 Information Detail"
                };

                context.ListNewsItems.Add(surveyItem);
            }

            context.SaveChanges();
        }
    }
}
