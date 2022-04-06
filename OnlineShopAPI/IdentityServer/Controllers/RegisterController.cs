using AutoMapper;
using IdentityServer.Data.Entities;
using IdentityServer.Data.Interfaces;
using IdentityServer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public RegisterController(
            IMapper mapper,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult<AppUser>> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            var existingUser = _userManager.Users.FirstOrDefault(u => u.NormalizedUserName == userRegisterDto.UserName.ToUpper());

            if (existingUser != null)
            {
                return BadRequest("User with this name already exists");
            }

            var newUser = _mapper.Map<AppUser>(userRegisterDto);

            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(_mapper.Map<UserDto>(newUser));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(_mapper.Map<IEnumerable<UserDto>>(_userManager.Users.ToList()));
        }
    }
}
