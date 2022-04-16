//using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShopAPI.DTO.Product;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _uow?.ProductRepository.GetAllAsync();

            if (products is null || products.Count() == 0)
            {
                return BadRequest("There are not any products");
            }

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{productName}")]
        public async Task<ActionResult<ProductDto>> GetProductByName(string productName)
        {
            var product = await _uow?.ProductRepository.GetProductByNameAsync(productName);

            if (product is null)
            {
                return BadRequest("Product not found");
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (string.IsNullOrEmpty(createProductDto?.Name))
            {
                return BadRequest("Product name cannot be null or empty");
            }

            var product = _mapper.Map<Product>(createProductDto);

            try
            {
                _uow?.ProductRepository.AddAsync(product);
            }
            catch
            {
                return BadRequest("Database error");
            }

            await _uow.ConfirmAsync();

            return Ok(product);
        }

        //[Authorize]
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

            product.Name = changeProductDto.Name;
            product.Amount = changeProductDto.Amount;
            product.PhotoUrl = changeProductDto.PhotoUrl;
            product.Description = changeProductDto.Description;

            _uow.ProductRepository.Update(product);

            await _uow.ConfirmAsync();

            return NoContent();
        }

        //[Authorize]
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
