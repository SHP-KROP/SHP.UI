using SHP.AuthorizationServer.Web.DTO.Auth.Google;
using SHP.AuthorizationServer.Web.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace SHP.AuthorizationServer.Web.Services
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
