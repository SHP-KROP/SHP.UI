using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SHP.OnlineShopAPI.Web.Constants;
using SHP.OnlineShopAPI.Web.DTO;
using SHP.OnlineShopAPI.Web.DTO.Product;
using SHP.OnlineShopAPI.Web.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHP.OnlineShopAPI.Web.Controllers
{
    [Authorize(Roles = Roles.AdminOrModerOrSeller)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ProductController(ILogger logger, IMapper mapper, IUnitOfWork uow)
        {
            _logger = logger;
            _mapper = mapper;
            _uow = uow;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _uow?.ProductRepository.GetAllAsync();

            if (products is null || products.Count() == 0)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost]
        [Route("range")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInRange(IdRangeModel range)
        {
            var products = await _uow?.ProductRepository.GetProductRangeById(range.Ids);

            if (products is null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("{productName}")]
        public async Task<ActionResult<ProductDto>> GetProductByName(string productName)
        {
            var product = await _uow?.ProductRepository.GetProductByNameAsync(productName);

            if (product is null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (string.IsNullOrEmpty(createProductDto?.Name))
            {
                return BadRequest("Product name cannot be null or empty");
            }

            var product = _mapper.Map<Product>(createProductDto);
            var user = await _uow.UserRepository.FindAsync(this.GetUserId());

            product.User = user;

            try
            {
                await _uow?.ProductRepository.AddAsync(product);
            }
            catch
            {
                return BadRequest("Database error");
            }

            await _uow?.ConfirmAsync();

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult> ChangeProduct(int id, [FromBody] ChangeProductDto changeProductDto)
        {
            var product = await _uow.ProductRepository.GetAsync(id);

            if (product is null)
            {
                return BadRequest(string.Format("Not found product with id {0}", id));
            }

            product.ProjectFrom(changeProductDto);

            _uow.ProductRepository.Update(product);

            await _uow.ConfirmAsync();

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{productName}")]
        public async Task<ActionResult<string>> DeleteProductByName(string productName)
        {
            var product = await _uow.ProductRepository.GetProductByNameAsync(productName);

            if (product is null)
            {
                return BadRequest(string.Format("Not found product with name {0}", productName));
            }

            try
            {
                _uow?.ProductRepository.Remove(product);
            }
            catch
            {
                return BadRequest("Database error");
            }

            await _uow.ConfirmAsync();

            return NoContent();
        }
    }
}
