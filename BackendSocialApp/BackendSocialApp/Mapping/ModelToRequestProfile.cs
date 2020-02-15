using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendSocialApp.Domain.Models;
using BackendSocialApp.Requests;

namespace BackendSocialApp.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            //CreateMap<Vendor, VendorResource>();
            //CreateMap<VendorUser, VendorUserResource>();
            //CreateMap<Employee, EmployeeResource>();
        }
    }
}
