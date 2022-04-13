//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            // TODO: Implement DA logic

            return new string[] { "value1", "value2" };
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{productName}")]
        public async Task<ActionResult<string>> GetCategoryByName(string productName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(productName));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<string>> CreateCategory([FromBody] string value)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(value));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{productName}")]
        public async Task<ActionResult<string>> ChangeCategory(string productName, [FromBody] string value)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(productName));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(string productName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(productName));
        }
    }
}
