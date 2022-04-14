//using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopAPI.DTO.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductController(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            // TODO: Implement DA logic

            return Ok(new ProductDto[] { new ProductDto { Name = "SomeName" } });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{productName}")]
        public async Task<ActionResult<ProductDto>> GetProductByName(string productName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(productName));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<string>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(Ok(createProductDto));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{productName}")]
        public async Task<ActionResult> ChangeProduct(string productName, [FromBody] ChangeProductDto changeProductDto)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(NoContent());
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteProductByName(string productName)
        {
            // TODO: Implement DA logic

            return await Task.FromResult<ActionResult>(NoContent());
        }
    }
}
