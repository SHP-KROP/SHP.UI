//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetProducts()
        {
            // TODO: Implement DA logic

            return new string[] { "value1", "value2" };
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{productName}")]
        public async Task<ActionResult<string>> GetProductByName(string productName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(productName));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<string>> CreateProduct([FromBody] string value)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(value));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{productName}")]
        public async Task<ActionResult<string>> ChangeProduct(string productName, [FromBody] string value)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(productName));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteProduct(string productName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(productName));
        }
    }
}
