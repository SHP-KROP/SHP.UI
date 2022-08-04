using AutoMapper;
using DAL.Interfaces;
using IdentityServer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Authorize(Roles = Roles.AdminOrModer)]
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ModeratorController> _logger;

        public ModeratorController(IUnitOfWork uow, IMapper mapper, ILogger<ModeratorController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers start executing");

            var users = await _uow.UserRepository.GetUsersAsync();

            if (users is null || !users.Any())
            {
                _logger.LogInformation("There were not any users. No content 204 returned");
                return NoContent();
            }

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            _logger.LogInformation("Usere have been found and mapped. Returned Ok 200");
            return Ok(userDtos);
        }
    }
}
