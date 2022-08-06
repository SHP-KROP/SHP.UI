using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using IdentityServer.DTO;
using IdentityServer.Extensions;
using IdentityServer.Services;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _uow;

        public UserController(
            IMapper mapper,
            ITokenService tokenService,
            IUnitOfWork uow
            )
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _uow = uow;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            var existingUser = await _uow.UserRepository.GetUserByUsernameAsync(userRegisterDto.UserName);

            if (existingUser != null)
            {
                return BadRequest("User with this name already exists");
            }

            var newUser = _mapper.Map<AppUser>(userRegisterDto);

            var result = await _uow.UserRepository.CreateUserAsync(newUser, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.ToErrorsString());
            }

            await _uow.UserRepository.AddToRoleAsync(newUser, "buyer");

            var roles = await _uow.UserRepository.GetUserRoles(newUser);

            var userDto = _mapper.Map<UserDto>(newUser);
            var token = _tokenService.CreateToken(newUser, roles);
            userDto.Token = token;

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn([FromBody] UserLogInDto userLogInDto)
        {
            var user = await _uow.UserRepository.GetUserByUsernameAsync(userLogInDto.UserName);

            if (user == null)
            {
                return Unauthorized($"There is not user with username {userLogInDto.UserName}");
            }

            var result = await _uow.SignInManager
                .CheckPasswordSignInAsync(user, userLogInDto.Password ?? string.Empty, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized("Wrong password");
            }

            var roles = await _uow.UserRepository.GetUserRoles(user);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user, roles);

            return Ok(userDto);
        }


        [HttpPost("redirect-to-auth")]
        public ActionResult<string> RedirectOnOAuthServer(OAuthModel model)
        { 
            var scope = "https://www.googleapis.com/auth/userinfo.email";

            Code.Init();

            var url = _tokenService.GenerateOAuthRequestUrl(scope, model.RedirectUrl, Code.CodeChallenge);
            return Ok(url);
        }

        public class Code
        {

            public static string CodeVerifier;

            public static string CodeChallenge;

            public static void Init()
            {
                CodeVerifier = GenerateNonce();
                CodeChallenge = GenerateCodeChallenge(CodeVerifier);
            }

            private static string GenerateNonce()
            {
                const string chars = "SecretString";
                var random = new Random();
                var nonce = new char[128];
                for (int i = 0; i < nonce.Length; i++)
                {
                    nonce[i] = chars[random.Next(chars.Length)];
                }

                return new string(nonce);
            }

            private static string GenerateCodeChallenge(string codeVerifier)
            {
                using var sha256 = SHA256.Create();
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                var b64Hash = Convert.ToBase64String(hash);
                var code = Regex.Replace(b64Hash, "\\+", "-");
                code = Regex.Replace(code, "\\/", "_");
                code = Regex.Replace(code, "=+$", "");
                return code;
            }
        }

        [HttpPost("google-register")]
        public async Task<ActionResult<TokenResult>> GoogleRegisterUser(string code)
        {
            var redirectUrl = HttpContext.Request.Host.ToUriComponent();
            var codeVerifier = Code.CodeVerifier;

            var tokenResult = await _tokenService.ExchangeCodeOnTokenAsync(code, codeVerifier, redirectUrl);


            //var dto = _tokenService.GetOAuthDtoFromToken(tokenResult);

            //var user = await _uow.UserRepository.GetUserByEmailAsync(dto.Email);

            //if (user != null)
            //{
            //    return BadRequest("User already exists");
            //}

            //var newUser = _mapper.Map<AppUser>(dto);
            //newUser.UserName = Guid.NewGuid().ToString();

            //var result = await _uow.UserRepository.CreateUserAsync(newUser, "pas$worD123456789426734683275235382");

            //if (!result.Succeeded)
            //{
            //    return BadRequest(result.ToErrorsString());
            //}

            //await _uow.UserRepository.AddToRoleAsync(newUser, "buyer");

            //var roles = await _uow.UserRepository.GetUserRoles(newUser);

            //var userDto = _mapper.Map<UserDto>(newUser);
            //var newToken = _tokenService.CreateToken(newUser, roles);
            //userDto.Token = newToken;

            return Ok(tokenResult);
        }

        //[HttpPost("google-login")]
        //public async Task<ActionResult<UserDto>> GoogleLoginUser(string token)
        //{
        //    var dto = _tokenService.GetOAuthDtoFromToken(token);

        //    var user = await _uow.UserRepository.GetUserByEmailAsync(dto.Email);

        //    if (user == null)
        //    {
        //        return Unauthorized($"There is not user with email {dto.Email}");
        //    }

        //    var roles = await _uow.UserRepository.GetUserRoles(user);
        //    var userDto = _mapper.Map<UserDto>(user);
        //    userDto.Token = _tokenService.CreateToken(user, roles);

        //    return Ok(userDto);
        //}

        [HttpPost("revoke-token")]
        public async Task<ActionResult> Revoke()
        {
            return Ok();
        }
    }
}
