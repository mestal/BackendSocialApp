﻿using AutoMapper;
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
            CreateMap<FortuneTellerUser, FortuneTellerViewModel>()
                .ForMember(dest => dest.UserStarPointAvg, opt => opt.MapFrom(src => src.UserStarPointCount != 0 ? Math.Round((double)src.UserStarPointTotal / (double)src.UserStarPointCount, 2) : 0));
            CreateMap<FortuneTellerUser, FortuneTellerDetailViewModel>()
                .ForMember(dest => dest.UserStarPointAvg, opt => opt.MapFrom(src => src.UserStarPointCount != 0 ? Math.Round((double)src.UserStarPointTotal / (double)src.UserStarPointCount, 2) : 0));
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
