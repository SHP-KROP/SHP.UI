using IdentityServer.Data.Entities;
using IdentityServer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace IdentityServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        const double HoursOfTokenExpiration = 12;

        public TokenService(IConfiguration configuration)
        {
            _config = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var roles = user?.UserRoles?.Select(ur => ur.Role.Name);

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.UserName),
                new Claim("roles", JsonSerializer.Serialize(roles ?? new List<string>{ })),
                new Claim("email", user.Email ?? string.Empty)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(HoursOfTokenExpiration),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
