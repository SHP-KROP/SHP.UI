using AutoMapper;
using IdentityServer.Data.Entities;
using IdentityServer.Data.Interfaces;
using IdentityServer.DTO;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly ISignInManager _signInManager;

        public UserController(
            IMapper mapper,
            ITokenService tokenService,
            IUserRepository userRepository,
            ISignInManager signInManager
            )
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {

            var existingUser = await _userRepository.GetUserByUsernameAsync(userRegisterDto.UserName);

            if (existingUser != null)
            {
                return BadRequest("User with this name already exists");
            }

            var newUser = _mapper.Map<AppUser>(userRegisterDto);

            var result = await _userRepository.CreateUserAsync(newUser, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var createdUser = await _userRepository.GetUserByUsernameAsync(userRegisterDto.UserName);

            await _userRepository.AddToRoleAsync(newUser, "buyer");

            var userDto = _mapper.Map<UserDto>(createdUser);
            var token = _tokenService.CreateToken(createdUser);
            userDto.Token = token;

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn([FromBody] UserLogInDto userLogInDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userLogInDto.UserName);

            if (user == null)
            {
                return Unauthorized($"There is not user with username {userLogInDto.UserName}");
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userLogInDto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized("Wrong password");
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            return Ok(userDto);
        }
    }
}
