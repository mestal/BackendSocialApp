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
            CreateMap<ConnectionStatus, string>()
                .ConvertUsing(src => Enum.GetName(typeof(ConnectionStatus), src));
            CreateMap<GenderType, string>()
                .ConvertUsing(src => Enum.GetName(typeof(GenderType), src));
            CreateMap<FortuneTellerViewModel, FortuneTellerUser>();
            CreateMap<FortuneTellerUserFalType, string>()
                .ConvertUsing(src => Enum.GetName(typeof(FortuneTellingType), src.FortunrTellingType) );
            CreateMap<FortuneTellerUser, FortuneTellerViewModel>()
                .ForMember(dest => dest.UserStarPointAvg, opt => opt.MapFrom(src => src.UserStarPointCount != 0 ? Math.Round((double)src.UserStarPointTotal / (double)src.UserStarPointCount, 1) : 0))
                .ForMember(s => s.FortuneTellingTypes, c => c.MapFrom(m => m.FalTypes));
            CreateMap<FortuneTellerUser, FortuneTellerDetailViewModel>()
                .ForMember(dest => dest.UserStarPointAvg, opt => opt.MapFrom(src => src.UserStarPointCount != 0 ? Math.Round((double)src.UserStarPointTotal / (double)src.UserStarPointCount, 1) : 0))
                .ForMember(s => s.FortuneTellingTypes, c => c.MapFrom(m => m.FalTypes));
            CreateMap<CoffeeFortuneTellingStatus, string>()
                .ConvertUsing(src => Enum.GetName(typeof(CoffeeFortuneTellingStatus), src));
            CreateMap<RelationshipStatus, string>()
                .ConvertUsing(src => Enum.GetName(typeof(RelationshipStatus), src));
            CreateMap<CoffeeFortuneTelling, CoffeeFortuneTellingViewModel>()
                .ForMember(dest => dest.Pictures, opt => opt.MapFrom(src => src.Pictures.Select(a => a.Path).ToList()));
            CreateMap<CoffeeFortuneTellingViewModel, CoffeeFortuneTelling>();
            CreateMap<ConsumerUser, UserViewModel>();
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<ConsumerUser, UserInfoViewModel>();
            CreateMap<AdminUser, UserViewModel>();
            CreateMap<MainFeed, FeedViewModel>()
                .ForMember(dest => dest.FeedType, opt => opt.MapFrom(src => src.GetType().ToString()));
            CreateMap<Survey, SurveyViewModel>();
            CreateMap<SurveyItem, SurveyItemViewModel>();
            CreateMap<SurveyItemAnswer, SurveyItemAnswerViewModel>();
            CreateMap<SurveyResultItem, SurveyResultItemViewModel>();
            CreateMap<News, NewsViewModel>();
            CreateMap<NewsItem, NewsItemViewModel>();
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.UserComment));
            CreateMap<Point, PointViewModel>();
        }
    }
}
