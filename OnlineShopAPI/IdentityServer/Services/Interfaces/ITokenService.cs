using DAL.Entities;
using IdentityServer.DTO;
using System.Collections.Generic;

namespace IdentityServer.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user, ICollection<string> roles);
        OAuthDto GetOAuthDtoFromToken(string token);
    }
}
