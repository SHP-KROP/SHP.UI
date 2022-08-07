using IdentityServer.DTO.Google;
using IdentityServer.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace IdentityServer.Services
{
    public class GoogleAuthService : IAuthService<GoogleOAuthDto>
    {
        public GoogleOAuthDto GetAuthDtoFromTokenId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenObject = tokenHandler.ReadJwtToken(token);
            var claims = tokenObject.Claims;

            string email = claims.Where(c => c.Type == "email").FirstOrDefault().Value;

            var oauthDto = new GoogleOAuthDto
            {
                Email = email
            };

            return oauthDto;
        }
    }
}
