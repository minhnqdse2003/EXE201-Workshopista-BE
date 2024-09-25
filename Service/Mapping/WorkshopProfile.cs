using AutoMapper;
using Repository.Models;
using Service.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class WorkshopProfile : Profile
    {
        public WorkshopProfile() 
        {
            CreateMap<Workshop,WorkShopCreateRequestModel>().ReverseMap();
            CreateMap<Workshop, WorkShopResponseModel>().ReverseMap();
        }
    }
}
