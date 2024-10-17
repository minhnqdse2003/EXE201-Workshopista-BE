using AutoMapper;
using Repository.Models;
using Service.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping.Resolver
{
    public class WorkshopImageProfile : Profile
    {
        public WorkshopImageProfile()
        {
            CreateMap<WorkshopImage, WorkshopImageResponseModel>();
        }
    }
}
