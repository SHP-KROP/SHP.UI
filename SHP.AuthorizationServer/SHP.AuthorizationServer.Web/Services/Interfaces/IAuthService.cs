using SHP.AuthorizationServer.Web.DTO.Auth;

namespace SHP.AuthorizationServer.Web.Services.Interfaces
{
    public interface IAuthService<TAuthDto> where TAuthDto : AuthDtoBase
    {
        TAuthDto GetAuthDtoFromTokenId(string token);
    }
}
