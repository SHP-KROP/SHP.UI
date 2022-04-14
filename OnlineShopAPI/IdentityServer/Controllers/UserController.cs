using AutoMapper;
using IdentityServer.Data.Entities;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(
            IMapper mapper,
            ITokenService tokenService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public AppUser GetAppUser(string userName)
        {
            return _userManager.Users.FirstOrDefault(u => u.NormalizedUserName == userName.ToUpper());
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {

            var existingUser = GetAppUser(userRegisterDto.UserName);

            if (existingUser != null)
            {
                //1
                return BadRequest("User with this name already exists");
            }

            var newUser = _mapper.Map<AppUser>(userRegisterDto);

            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                //2
                return BadRequest(result.Errors);
            }

            var createdUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == newUser.UserName);

            await _userManager.AddToRoleAsync(newUser, "buyer");

            var userDto = _mapper.Map<UserDto>(createdUser);
            var token = _tokenService.CreateToken(createdUser);
            userDto.Token = token;

            //3
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn([FromBody] UserLogInDto userLogInDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(
                user => user.NormalizedUserName == userLogInDto.UserName.ToUpper()
                );

            if (user == null)
            {
                //1
                return Unauthorized($"There is not user with username {userLogInDto.UserName}");
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, userLogInDto.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                //2
                return Unauthorized("Wrong password");
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);

            //3
            return Ok(userDto);
        }
    }
}
