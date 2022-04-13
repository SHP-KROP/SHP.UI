//using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopAPI.DTO.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CategoryController(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            // TODO: Implement DA logic

            return Ok(new CategoryDto[] {});
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{productName}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByName(string categoryName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(new CategoryDto()));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(new CategoryDto()));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{productName}")]
        public async Task<ActionResult> ChangeCategory(string categoryName, [FromBody] ChangeCategoryDto changeCategoryDto)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(NoContent());
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCategory(string categoryName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(NoContent());
        }
    }
}
