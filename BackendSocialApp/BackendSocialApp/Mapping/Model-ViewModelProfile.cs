using AutoMapper;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSocialApp.Mapping
{
    public class Model_ViewModelProfile : Profile
    {
        public Model_ViewModelProfile()
        {
            CreateMap<ConnectionStatus, string>().ConvertUsing(src => Enum.GetName(typeof(ConnectionStatus), src));
            CreateMap<FortuneTellerViewModel, FortuneTellerUser>();
            CreateMap<FortuneTellerUser, FortuneTellerViewModel>();

            CreateMap<CoffeeFortuneTellingStatus, string>().ConvertUsing(src => Enum.GetName(typeof(CoffeeFortuneTellingStatus), src));
            CreateMap<GenderType, string>().ConvertUsing(src => Enum.GetName(typeof(GenderType), src));

            CreateMap<CoffeeFortuneTelling, CoffeeFortuneTellingViewModel>();
            CreateMap<CoffeeFortuneTellingViewModel, CoffeeFortuneTelling>();
            CreateMap<ConsumerUser, UserViewModel>();
            CreateMap<ConsumerUser, UserInfoViewModel>();
            CreateMap<MainFeed, FeedViewModel>().ForMember(dest => dest.FeedType, opt => opt.MapFrom(src => src.GetType().ToString()));
            CreateMap<Survey, SurveyViewModel>();
            CreateMap<SurveyItem, SurveyItemViewModel>();
            CreateMap<SurveyItemAnswer, SurveyItemAnswerViewModel>();
            CreateMap<SurveyResultItem, SurveyResultItemViewModel>();
            CreateMap<News, NewsViewModel>();
            CreateMap<NewsItem, NewsItemViewModel>();
        }
    }
}
