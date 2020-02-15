using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Requests;

namespace BackendSocialApp.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<CreateCoffeeFortuneTellingRequest, CoffeeFortuneTelling>()
                .ForMember(c => c.Pictures, option => option.Ignore());
            ;


        }
    }
}
