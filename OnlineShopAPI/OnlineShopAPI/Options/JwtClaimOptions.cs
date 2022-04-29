using System.IdentityModel.Tokens.Jwt;

namespace OnlineShopAPI.Options
{
    public static class JwtClaimOptions
    {
        public const string NameId = "nameid";
        public const string Name = JwtRegisteredClaimNames.NameId;
        public const string Roles = "roles";
        public const string Email = JwtRegisteredClaimNames.Email;
        public const string AuthorizationNameId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    }
}
