using AutoMapper;
using Repository.Consts;
using Repository.Models;
using Service.Models.Category;
using Service.Models.Organizers;
using Service.Models.TicketRank;
using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<User, PostUserModel>().ReverseMap();
            CreateMap<UserRegisterModel, User>().ForMember(u => u.UserId, otp => otp.MapFrom(src => Guid.NewGuid()));
            CreateMap<OrganizerRegisterModel, User>().ForMember(u => u.UserId, otp => otp.MapFrom(src => Guid.NewGuid()));

            //Organizer
            CreateMap<OrganizerRegisterModel, Organizer>().ForMember(u => u.OrganizerId, otp => otp.MapFrom(src => Guid.NewGuid()));
            CreateMap<UpdateOrganizerModel, Organizer>();

            //Category
            CreateMap<CategoryRequestModel, Category>().ForMember(u => u.UpdatedAt, otp => otp.MapFrom(src => DateTime.Now));

            //TicketRank
            CreateMap<TicketRankRequestModel, TicketRank>().ForMember(u => u.UpdatedAt, otp => otp.MapFrom(src => DateTime.Now)).ReverseMap();
        }
    }
}
