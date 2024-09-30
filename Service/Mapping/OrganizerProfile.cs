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
    public class OrganizerProfile : Profile
    {
        public OrganizerProfile()
        {
            CreateMap<OrganizerRegisterRequestModel, Organizer>()
                .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Verified, opt => opt.MapFrom(src => false));
            CreateMap<Organizer, OrganizerResponseModel>().ReverseMap();
            CreateMap<OrganizerUpdateRequestModel, Organizer>()
                .ForMember(dest => dest.OrganizerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
