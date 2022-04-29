using DAL.Entities;
using IdentityServer.Options;
using IdentityServer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[ConfigurationOptions.Token]));
        }

        public string CreateToken(AppUser user, ICollection<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimOptions.NameId, user.Id.ToString()),
                new Claim(JwtClaimOptions.Name, user.UserName),
                new Claim(JwtClaimOptions.Roles, JsonSerializer.Serialize(roles ?? new List<string>{ })),
                new Claim(JwtClaimOptions.Email, user.Email ?? string.Empty)
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
