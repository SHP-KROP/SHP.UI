using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using IdentityServer.Constants;
using IdentityServer.DTO;
using IdentityServer.DTO.Google;
using IdentityServer.Extensions;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var token = _tokenService.CreateToken(newUser, roles);
            userDto.Token = token;

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
            userDto.Token = _tokenService.CreateToken(user, roles);

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

            if (user != null)
            {
                userDto = _mapper.Map<UserDto>(user);
                userDto.Token = _tokenService.CreateToken(user, await _uow.UserRepository.GetUserRoles(user));

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

            userDto.Token = _tokenService.CreateToken(newUser, await _uow.UserRepository.GetUserRoles(newUser));

            return Ok(userDto);
        }

        [HttpPost("revoke-token")]
        public async Task<ActionResult> Revoke()
        {
            return Ok();
        }
    }
}
