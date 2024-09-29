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
    class TicketRankProfile : Profile
    {
        public TicketRankProfile()
        {
            CreateMap<TicketRank,WorkshopTicketRankRegisterRequestModel>().ReverseMap();
            CreateMap<TicketRank, TicketRankModelResponse>().ReverseMap();
            CreateMap<TicketRankUpdateModel, TicketRank>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.TicketRankId, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
