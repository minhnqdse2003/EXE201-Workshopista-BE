using AutoMapper;
using Repository.Models;
using Service.Mapping.Resolver;
using Service.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketDto>()
                 .ForMember(dest => dest.QrCode, opt => opt.MapFrom<QRCodeResolver>());

            CreateMap<TicketUpdateModel, Ticket>()
                .ForMember(dest => dest.TicketId, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
