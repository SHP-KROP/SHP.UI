using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        // POST api/<RegisterController>
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            return Ok();
        }

        [HttpGet("Ok")]
        public ActionResult GetOk()
        {
            return Ok();
        }
    }
}
