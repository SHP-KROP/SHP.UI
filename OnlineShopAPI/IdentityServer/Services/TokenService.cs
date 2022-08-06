using DAL.Entities;
using IdentityServer.DTO;
using IdentityServer.Options;
using IdentityServer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        private const string ClientId = "211105299474-e1e6i4h683dlo69c3kko34cd2o4cct7d.apps.googleusercontent.com";
        private const string ClientSecret = "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";
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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

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

        public OAuthDto GetOAuthDtoFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenObject = tokenHandler.ReadJwtToken(token);

            var claims = tokenObject.Claims;

            string email = claims.Where(c => c.Type == "email").FirstOrDefault().Value;

            var oauthDto = new OAuthDto
            {
                Email = email
            };

            return oauthDto;
        }

        public string GenerateOAuthRequestUrl(string scope, string redirectUrl, string codeChallenge)
        {
            var url = "https://accounts.google.com/o/oauth2/v2/auth?";

            var queryParams = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "redirect_uri", redirectUrl },
                { "response_type", "code" },
                { "scope", scope },
                { "code_challenge", codeChallenge },
                { "code_challenge_method", "S256" }
            };

            foreach (var qp in queryParams)
            {
                url += $"{qp.Key}={qp.Value}&";
            }

            return url.Substring(0, url.Length - 1);
        }

        public async Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier, string redirectUrl)
        {
            var url = "https://oauth2.googleapis.com/token";

            var authParams = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "code", code },
                { "code_verifier", codeVerifier },
                { "grant_type", "authorization_code" },
                { "redirect_uri", redirectUrl }
            };

            var httpContent = new FormUrlEncodedContent(authParams);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (httpContent != null)
            {
                request.Content = httpContent;
            }
            using var httpClient = new HttpClient();
            using var response = await httpClient.SendAsync(request);
            var resultJson = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(resultJson);
            }
            var result = JsonConvert.DeserializeObject<TokenResult>(resultJson);

            var tokenResult = JsonConvert.DeserializeObject<TokenResult>(resultJson);
            return tokenResult;
        }
    }
    public class TokenResult
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")] 
        public string ExpiresIn { get; set; }
        [JsonProperty("scope")] 
        public string Scope { get; set; }
        [JsonProperty("token_type")] 
        public string TokenType { get; set; }
        [JsonProperty("refresh_token")] 
        public string RefreshToken { get; set; }
    }
}
