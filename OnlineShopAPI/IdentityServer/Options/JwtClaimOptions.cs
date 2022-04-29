using System.IdentityModel.Tokens.Jwt;

namespace IdentityServer.Options
{
    public static class JwtClaimOptions
    {
        public const string NameId = "nameid";
        public const string Name = JwtRegisteredClaimNames.NameId;
        public const string Roles = "roles";
        public const string Email = JwtRegisteredClaimNames.Email;
    }
}
