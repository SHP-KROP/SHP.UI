using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [Authorize(Roles = Roles.AdminOrModer)]
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        public ModeratorController()
        {

        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Ok();
        }
    }
}
