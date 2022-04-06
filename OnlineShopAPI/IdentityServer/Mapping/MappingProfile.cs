using AutoMapper;
using IdentityServer.Data.Entities;
using IdentityServer.DTO;
using System.Collections.Generic;

namespace IdentityServer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, AppUser>();
            CreateMap<AppUser, UserRegisterDto>();

            CreateMap<AppUser, UserDto>();
            CreateMap<UserDto, AppUser>();
        }
    }
}
