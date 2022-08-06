using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.Constants;
using OnlineShopAPI.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopAPI.Controllers
{
    [Authorize(Roles = Roles.Buyer)]
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LikeController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetLikedProductsByUser()
        {
            var products = await _uow?.UserRepository.GetProductsLikedByUser(this.GetUserId());

            if (products is null || !products.Any())
            {
                return NoContent();
            }

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            productDtos.ToList().ForEach(pr => pr.IsLiked = true);

            return Ok(productDtos);
        }

        [HttpGet("product")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsWithLikes()
        {
            var likedProducts = await _uow?.UserRepository.GetProductsLikedByUser(this.GetUserId());
            var products = await _uow?.ProductRepository.GetAllAsync();

            if (products is null || !products.Any())
            {
                return NoContent();
            }

            Func<Product, ProductDto> productAndSetIsLiked = (product) =>
            {
                var productDto = _mapper.Map<ProductDto>(product);
                productDto.IsLiked = likedProducts.Contains(product);

                return productDto;
            };

            var productsWithLikeInfo = products.Select(productAndSetIsLiked);

            return Ok(productsWithLikeInfo);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{productId}")]
        public async Task<ActionResult<ProductDto>> LikeProduct(int productId)
        {
            var product = await _uow?.ProductRepository.LikeProductByUser(this.GetUserId(), productId);

            return await _uow?.ConfirmAsync() 
                ? Ok(_mapper.Map<ProductDto>(product))
                : BadRequest("Unable to like the product");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ProductDto>> UnlikeProduct(int productId)
        {
            var product = await _uow?.ProductRepository.UnlikeProductByUser(this.GetUserId(), productId);

            return await _uow?.ConfirmAsync()
                ? Ok(_mapper.Map<ProductDto>(product))
                : BadRequest("Unable to unlike the product");
        }
    }
}
