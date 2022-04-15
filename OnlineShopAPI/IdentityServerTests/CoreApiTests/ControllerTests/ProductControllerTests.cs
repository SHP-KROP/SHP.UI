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
            // TODO: Add when DAL injected
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

            var products = await _productController.GetProducts() as ActionResult<IEnumerable<ProductDto>>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as IEnumerable<ProductDto>;

            value?.Should().NotBeNull();
            value?.Count().Should().Be(0);

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetProductByName_ShouldReturnOk_WhenProductWithProperNameFound()
        {
            // TODO: Add when DAL injected

            var products = await _productController.GetProductByName("SomeName") as ActionResult<ProductDto>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as ProductDto;

            value?.Should().NotBeNull();
            value?.Should().BeOfType(typeof(ProductDto));
            value?.Name.Should().Be("SomeName");

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetProductByName_ShouldReturnBadRequest_WhenProductNotFound()
        {
            // TODO: Add when DAL injected

            var products = await _productController.GetProductByName(It.IsAny<string>()) as ActionResult<ProductDto>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as ProductDto;

            value?.Should().BeNull();

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnOkWithProduct_WhenProductIsValid()
        {
            // TODO: Add when DAL injected

            var products = await _productController.CreateProduct(GetValidCreateProductDto()) as ActionResult<ProductDto>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as ProductDto;

            value?.Should().NotBeNull();
            value?.Should().BeOfType(typeof(ProductDto));
            value?.Should().BeEquivalentTo(GetValidCreateProductDto());

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnBadRequest_WhenProductHasEmptyName()
        {
            // TODO: Add when DAL injected

            var products = await _productController.CreateProduct(GetInvalidCreateProductDtoWithEmptyName()) as ActionResult<ProductDto>;

            var result = products.Result as ObjectResult;
            var value = result?.Value;

            value?.Should().Be("Unable to create product with empty mandatory fields");
            
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ChangeProduct_ShouldReturnBadRequest_WhenProductWithNameNotFound()
        {
            // TODO: Add when DAL injected

            var productName = "SomeName";

            var result = await _productController.ChangeProduct(productName, GetValidChangeProductDto()) as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            result.Value.Should().Be(string.Format("Not found product with name {0}", productName));
        }

        private ChangeProductDto GetValidChangeProductDto() => new ChangeProductDto
        {
            Name = "SomeNewName",
            Amount = 100,
            IsAvaliable = true,
            Description = "SomeDescription",
            PhotoUrl = "SomeUrl"
        };

        [Fact]
        public async Task DeleteProductByName_ShouldReturnBadRequest_WhenProductNameDontMatchAnyProduct()
        {
            // TODO: Add when DAL injected

            string productName = "SomeProduct";

            var products = await _productController.DeleteProductByName(productName) as ActionResult<string>;

            var result = products.Result as ObjectResult;
            var value = result?.Value;

            value?.Should().Be(string.Format("Not found product with name {0}", productName));

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DeleteProductByName_ShouldReturnNoContent_WhenProductDeletedSuccessfully()
        {
            // TODO: Add when DAL injected

            string productName = "SomeProduct";

            var products = await _productController.DeleteProductByName(productName) as ActionResult<string>;

            var result = products.Result as ObjectResult;
            var value = result?.Value;

            value?.Should().BeNull();

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        private CreateProductDto GetValidCreateProductDto() => new CreateProductDto
        {
            Name = "SomeName",
            Amount = 500,
            IsAvaliable = true,
            Description = "SomeDescription",
            PhotoUrl = "SomeUrl"
        };

        private CreateProductDto GetInvalidCreateProductDtoWithEmptyName()
        {
            var createProductDto = GetValidCreateProductDto();
            createProductDto.Name = string.Empty;
            return createProductDto;
        }
    }
}
