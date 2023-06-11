using AutoMapper;
using DAL.Interfaces;
using SHP.AuthorizationServer.Web.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHP.AuthorizationServer.Web.Controllers
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers start executing");

            var users = await _uow.UserRepository.GetUsersAsync();

            if (users is null || !users.Any())
            {
                _logger.LogInformation("There were not any users");
                return NoContent();
            }

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(userDtos);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("DeleteUser start executing");

            var user = await _uow.UserRepository.FindAsync(id);

            if (user is null)
            {
                _logger.LogInformation($"There is not user with id {id}");
                return NoContent();
            }

            var roles = await _uow.UserRepository.GetUserRoles(user);

            if (roles.Where(r => r == Roles.Admin || r == Roles.Moder).Any())
            {
                return BadRequest("Unable to delete user with site-managing role");
            }

            _uow.UserRepository.RemoveUserById(id);

            if (await _uow.ConfirmAsync())
            {
                return Ok();
            }

            _logger.LogInformation("Unable to save changes after deletion");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
