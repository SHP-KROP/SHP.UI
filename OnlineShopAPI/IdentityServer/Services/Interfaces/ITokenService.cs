using IdentityServer.Data.Entities;

namespace IdentityServer.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
