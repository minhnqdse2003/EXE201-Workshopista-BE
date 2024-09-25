using AutoMapper;
using Repository.Models;
using Service.Models.Users;
using Service.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, PostUserModel>().ReverseMap();
            CreateMap<User, UserResponseModel>().ReverseMap();
        }
    }
}
