using IdentityServer.DTO.Auth;

namespace IdentityServer.Services.Interfaces
{
    public interface IAuthService<TAuthDto> where TAuthDto : AuthDtoBase
    {
        TAuthDto GetAuthDtoFromTokenId(string token);
    }
}
