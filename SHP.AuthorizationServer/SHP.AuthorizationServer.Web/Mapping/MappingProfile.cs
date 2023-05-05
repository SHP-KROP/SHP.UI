using AutoMapper;
using DAL.Entities;
using SHP.AuthorizationServer.Web.DTO;

namespace SHP.AuthorizationServer.Web.Mapping
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
