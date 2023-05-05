using System;

namespace SHP.AuthorizationServer.Web.Options
{
    public class JwtOptions
    {
        public const string Jwt = "Jwt";

        public string TokenKey { get; set; } = string.Empty;

        public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromMinutes(3);

        public int RefreshTokenExpirationMonths { get; set; } = 3;
    }
}
