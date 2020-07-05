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
            
        }
    }
}
