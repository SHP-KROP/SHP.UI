using AutoMapper;
using DAL.Entities;
using IdentityServer.DTO;

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
