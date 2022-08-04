using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Authorize(Roles = Roles.AdminOrModer)]
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {

    }
}
