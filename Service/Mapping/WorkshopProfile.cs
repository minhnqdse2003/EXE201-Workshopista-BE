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
            CreateMap<Workshop, WorkShopCreateRequestModel>().ReverseMap();
            CreateMap<Workshop, WorkShopResponseModel>().ReverseMap();
            CreateMap<WorkShopUpdateRequestModel, Workshop>()
                .ForMember(dest => dest.WorkshopId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())  
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                .ForMember(dest => dest.TicketRanks, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
