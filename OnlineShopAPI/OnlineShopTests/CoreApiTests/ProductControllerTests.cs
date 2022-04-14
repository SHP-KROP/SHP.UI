using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineShopAPI.Controllers;
using OnlineShopAPI.DTO.Product;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopAPI.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;

        public ProductControllerTests()
        {
            var mockLogger = new Mock<ILogger>();
            var mockMapper = new Mock<IMapper>();

            _productController = new ProductController(mockLogger.Object, mockMapper.Object);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOkWithNotEmptyProducts()
        {
            var products = await _productController.GetProducts() as ActionResult<IEnumerable<ProductDto>>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as IEnumerable<ProductDto>;

            value?.Should().NotBeNull();
            value?.Count().Should().BeGreaterThan(0);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnBadRequest_WhenProductCountIsZero()
        {
            // TODO: Add when DAL injected

            true.Should().BeFalse();
        }
    }
}
