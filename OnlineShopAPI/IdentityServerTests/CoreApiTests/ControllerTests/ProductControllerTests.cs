using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineShopAPI.Controllers;
using OnlineShopAPI.DTO.Product;
using OnlineShopAPI.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopAPI.Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IProductRepository> _productRepository;

        public ProductControllerTests()
        {
            var mockLogger = new Mock<ILogger>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper() as IMapper;
            _mockUow = new Mock<IUnitOfWork>();
            _productRepository = new Mock<IProductRepository>();
            _mockUow.SetupGet(uow => uow.ProductRepository).Returns(_productRepository.Object);

            _productController = new ProductController(mockLogger.Object, mapper, _mockUow.Object);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOk_WithNotEmptyProducts()
        {
            _productRepository
                .Setup(pr => pr.GetAllAsync())
                .ReturnsAsync(new List<Product> { new Product() });

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
            _productRepository
                .Setup(pr => pr.GetAllAsync())
                .ReturnsAsync(new List<Product> {});

            var products = await _productController.GetProducts() as ActionResult<IEnumerable<ProductDto>>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as string;

            value?.Should().Be("There are not any products");

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetProductByName_ShouldReturnOk_WhenProductWithProperNameFound()
        {
            _productRepository
                .Setup(pr => pr.GetProductByNameAsync("SomeName"))
                .ReturnsAsync(new Product { Name = "SomeName" });

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
            _productRepository
                .Setup(pr => pr.GetProductByNameAsync("SomeName"))
                .ReturnsAsync(null as Product);

            var products = await _productController.GetProductByName(It.IsAny<string>()) as ActionResult<ProductDto>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as string;

            value?.Should().Be("Product not found");

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnOkWithProduct_WhenProductIsValid()
        {

            
            var products = await _productController.CreateProduct(GetValidCreateProductDto()) as ActionResult<Product>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as ProductDto;

            value?.Should().NotBeNull();
            value?.Should().BeOfType(typeof(Product));
            value?.Should().BeEquivalentTo(GetValidCreateProductDto());

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnBadRequest_WhenProductHasEmptyName()
        {
            var products = await _productController
                .CreateProduct(GetInvalidCreateProductDtoWithEmptyName()) as ActionResult<Product>;

            var result = products.Result as ObjectResult;
            var value = result?.Value;

            value?.Should().Be("Product name cannot be null or empty");
            
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ChangeProduct_ShouldReturnBadRequest_WhenProductWithNameNotFound()
        {

            var wrongProductName = "wrong name";

            _productRepository
                .Setup(pr => pr.GetProductByNameAsync(wrongProductName))
                .ReturnsAsync(null as Product);

            var result = await _productController.ChangeProduct(wrongProductName, GetValidChangeProductDto()) as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            result.Value.Should().Be(string.Format("Not found product with name {0}", wrongProductName));
        }

        [Fact]
        public async Task ChangeProduct_ShouldReturnNoContent_WhenProductValid()
        {

            var properName = "proper name";

            _productRepository
                .Setup(pr => pr.GetProductByNameAsync(properName))
                .ReturnsAsync(new Product { Id = 3, Name = "proper name"});

            var result = await _productController
                .ChangeProduct(properName, GetValidChangeProductDto()) as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
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
            string productName = "SomeProduct";

            _productRepository
                .Setup(pr => pr.GetProductByNameAsync(productName))
                .ReturnsAsync(null as Product);

            var products = await _productController.DeleteProductByName(productName) as ActionResult<string>;

            var result = products.Result as ObjectResult;
            var value = result?.Value;

            value?.Should().Be(string.Format("Not found product with name {0}", productName));

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DeleteProductByName_ShouldReturnNoContent_WhenProductDeletedSuccessfully()
        {
            string productName = "SomeProduct";

            _productRepository
                .Setup(pr => pr.GetProductByNameAsync(productName))
                .ReturnsAsync(new Product());

            var products = await _productController.DeleteProductByName(productName) as ActionResult<string>;

            var result = products.Result as StatusCodeResult;

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
