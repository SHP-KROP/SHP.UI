using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SHP.OnlineShopAPI.Web.Controllers;
using SHP.OnlineShopAPI.Web.DTO;
using SHP.OnlineShopAPI.Web.DTO.Product;
using SHP.OnlineShopAPI.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SHP.OnlineShopAPI.Tests.ControllerTests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IUserRepository> _userRepository;

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

            _userRepository = new Mock<IUserRepository>();
            _mockUow.SetupGet(uow => uow.UserRepository).Returns(_userRepository.Object);

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
        public async Task GetProducts_ShouldReturnNoContent_WhenProductCountIsZero()
        {
            _productRepository
                .Setup(pr => pr.GetAllAsync())
                .ReturnsAsync(new List<Product> {});

            var products = await _productController.GetProducts() as ActionResult<IEnumerable<ProductDto>>;

            var result = products.Result as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task GetProductsInRange_ShouldReturnNoContent_WhenProductCountIsZero()
        {
            _productRepository
                .Setup(pr => pr.GetProductRangeById(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(null as List<Product>);

            var products = await _productController.GetProductsInRange(new IdRangeModel { Ids = new List<int> { 1 } }) as ActionResult<IEnumerable<ProductDto>>;

            var result = products.Result as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task GetProductsInRange_ShouldReturnOk_WithNotEmptyProducts()
        {
            _productRepository
                .Setup(pr => pr.GetProductRangeById(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<Product> { new Product() });

            var products = await _productController.GetProductsInRange(new IdRangeModel { Ids = new List<int> { 1 } }) as ActionResult<IEnumerable<ProductDto>>;

            var result = products.Result as ObjectResult;
            var value = result?.Value as IEnumerable<ProductDto>;

            value?.Should().NotBeNull();
            value?.Count().Should().BeGreaterThan(0);

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
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
        public async Task GetProductByName_ShouldReturnNoContent_WhenProductNotFound()
        {
            _productRepository
                .Setup(pr => pr.GetProductByNameAsync("SomeName"))
                .ReturnsAsync(null as Product);

            var products = await _productController.GetProductByName(It.IsAny<string>()) as ActionResult<ProductDto>;

            var result = products.Result as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnOkWithProduct_WhenProductIsValid()
        {
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
            var products = await _productController
                .CreateProduct(GetInvalidCreateProductDtoWithEmptyName()) as ActionResult<ProductDto>;

            var result = products.Result as ObjectResult;
            var value = result?.Value;

            value?.Should().Be("Product name cannot be null or empty");
            
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnBadRequest_WhenAddAsyncThrowsException()
        {
            _productRepository.Setup(pr => pr.AddAsync(It.IsAny<Product>())).Throws<Exception>();

            var products = await _productController
                .CreateProduct(GetValidCreateProductDto()) as ActionResult<ProductDto>;

            var result = products.Result as ObjectResult;
            var value = result?.Value;

            value?.Should().Match<string>(str => str.Contains("Database"))
                .And.Match<string>(str => str.Contains("error"));

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task ChangeProduct_ShouldReturnBadRequest_WhenProductWithNameNotFound()
        {

            var wrongId = -1;

            _productRepository
                .Setup(pr => pr.GetAsync(wrongId))
                .ReturnsAsync(null as Product);

            var result = await _productController.ChangeProduct(wrongId, GetValidChangeProductDto()) as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

            result.Value.Should().Be(string.Format("Not found product with id {0}", wrongId));
        }

        [Fact]
        public async Task ChangeProduct_ShouldReturnNoContent_WhenProductValid()
        {

            var properId = 3;

            _productRepository
                .Setup(pr => pr.GetAsync(properId))
                .ReturnsAsync(new Product { Id = 3, Name = "proper name"});

            var result = await _productController
                .ChangeProduct(properId, GetValidChangeProductDto()) as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        private ChangeProductDto GetValidChangeProductDto() => new ChangeProductDto
        {
            Name = "SomeNewName",
            Amount = 100,
            IsAvailable = true,
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

        [Fact]
        public async Task DeleteProductByName_ShouldReturnBadRequest_WhenUowThrowsException()
        {
            string productName = "SomeProduct";
            var product = new Product { Name = productName };

            _productRepository
                .Setup(pr => pr.GetProductByNameAsync(productName))
                .ReturnsAsync(product);

            _productRepository.Setup(pr => pr.Remove(It.Is<Product>(p => p.Name == productName))).Throws<Exception>();

            var response = await _productController.DeleteProductByName(productName) as ActionResult<string>;

            ((ObjectResult)response.Result).StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        private CreateProductDto GetValidCreateProductDto() => new CreateProductDto
        {
            Name = "SomeName",
            Amount = 500,
            IsAvailable = true,
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
