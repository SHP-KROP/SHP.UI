using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using SHP.AuthorizationServer.Web.Constants;
using SHP.AuthorizationServer.Web.DTO;
using SHP.AuthorizationServer.Web.DTO.Auth.Google;
using SHP.AuthorizationServer.Web.Extensions;
using SHP.AuthorizationServer.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHP.AuthorizationServer.Web.Contracts;
using System;
using System.Threading.Tasks;

namespace SHP.AuthorizationServer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _uow;
        private readonly IAuthService<GoogleOAuthDto> _googleAuthService;

        public UserController(
            IMapper mapper,
            ITokenService tokenService,
            IUnitOfWork uow,
            IAuthService<GoogleOAuthDto> googleAuthService
            )
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _uow = uow;
            _googleAuthService = googleAuthService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            var existingUser = await _uow.UserRepository.GetUserByUsernameAsync(userRegisterDto.UserName);

            if (existingUser != null)
            {
                return Unauthorized("User with this name already exists");
            }

            var newUser = _mapper.Map<AppUser>(userRegisterDto);

            var result = await _uow.UserRepository.CreateUserAsync(newUser, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                return Unauthorized(result.ToErrorsString());
            }

            await _uow.UserRepository.AddToRoleAsync(newUser, "buyer");

            var roles = await _uow.UserRepository.GetUserRoles(newUser);

            var userDto = _mapper.Map<UserDto>(newUser);
            var authResult = await _tokenService.CreateToken(newUser, roles);

            if (!authResult.Success)
            {
                return Unauthorized();
            }

            userDto.Token = authResult.Token;
            userDto.RefreshToken = authResult.RefreshToken;

            return Ok(userDto);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
            var authResult = await _tokenService.CreateToken(user, roles);

            if (!authResult.Success)
            {
                return Unauthorized();
            }

            userDto.Token = authResult.Token;
            userDto.RefreshToken = authResult.RefreshToken;

            return Ok(userDto);
        }

        [HttpPost("google-auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> GoogleAuthUser([FromQuery] string tokenId)
        {
            var oAuthDto = _googleAuthService.GetAuthDtoFromTokenId(tokenId);
            var user = await _uow.UserRepository.GetUserByEmailAsync(oAuthDto.Email);

            UserDto userDto;
            AuthenticationResult authResult;

            if (user != null)
            {
                userDto = _mapper.Map<UserDto>(user);
                authResult = await _tokenService.CreateToken(user, await _uow.UserRepository.GetUserRoles(user));

                if (!authResult.Success)
                {
                    return Unauthorized();
                }

                userDto.Token = authResult.Token;
                userDto.RefreshToken = authResult.RefreshToken;

                return Ok(userDto);
            }

            var newUser = new AppUser
            {
                UserName = Guid.NewGuid().ToString(),
                Email = oAuthDto.Email
            };

            var result = await _uow.UserRepository.CreateUserAsync(newUser, UserDefaultConstants.Password);

            if (!result.Succeeded)
            {
                return Unauthorized(result.ToErrorsString());
            }

            await _uow.UserRepository.AddToRoleAsync(newUser, "buyer");
            userDto = _mapper.Map<UserDto>(newUser);

            authResult = await _tokenService.CreateToken(newUser, await _uow.UserRepository.GetUserRoles(newUser));

            if (!authResult.Success)
            {
                return Unauthorized();
            }

            userDto.Token = authResult.Token;
            userDto.RefreshToken = authResult.RefreshToken;

            return Ok(userDto);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResult>> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResult = await _tokenService.RefreshToken(request.Token, request.RefreshToken);

            if (!authResult.Success)
            {
                return BadRequest(authResult.Errors);
            }

            return Ok(authResult);
        }

        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Revoke([FromQuery] string refreshToken)
        {
            var authResult = await _tokenService.RevokeToken(refreshToken);

            if (!authResult.Success)
            {
                return BadRequest(authResult.Errors);
            }

            return NoContent();
        }
    }
}
