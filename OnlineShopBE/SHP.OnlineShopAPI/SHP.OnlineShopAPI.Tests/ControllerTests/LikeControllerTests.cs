using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SHP.OnlineShopAPI.Web.Controllers;
using SHP.OnlineShopAPI.Web.DTO.Product;
using SHP.OnlineShopAPI.Web.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SHP.OnlineShopAPI.Tests.ControllerTests
{
    public class LikeControllerTests
    {
        private readonly LikeController _likeController;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IUserRepository> _userRepository;

        public LikeControllerTests()
        {
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

            _likeController = new LikeController(_mockUow.Object, mapper);
        }

        [Fact]
        public async Task GetLikedProductsByUser_ShouldReturnOkWithProductList_WhenLikedProductsCountGreaterThanZero()
        {
            _userRepository
                .Setup(p => p.GetProductsLikedByUser(It.IsAny<int>()))
                .ReturnsAsync(new List<Product> { new Product() });

            var result = await _likeController.GetLikedProductsByUser();
            var objResult = (result.Result as ObjectResult);
            var likedProducts = (objResult.Value as IEnumerable<ProductDto>);

            likedProducts.Should().NotBeNullOrEmpty();
            objResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetLikedProductsByUser_ShouldReturnNoContent_WhenLikedProductsAreNotPresent()
        {
            _userRepository
                .Setup(p => p.GetProductsLikedByUser(It.IsAny<int>()))
                .ReturnsAsync(new List<Product>());

            var result = await _likeController.GetLikedProductsByUser();
            var statusCodeResult = (result.Result as StatusCodeResult);

            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task LikeProduct_ShouldReturnOk_WhenAbleToLikeProduct()
        {
            _productRepository
                .Setup(p => p.LikeProductByUser(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Product());

            _mockUow.Setup(uow => uow.ConfirmAsync()).ReturnsAsync(true);

            var result = await _likeController.LikeProduct(It.IsAny<int>());

            var objResult = (result.Result as ObjectResult);

            objResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task LikeProduct_ShouldReturnBadRequest_WhenUnableToLikeProduct()
        {
            _productRepository
                .Setup(p => p.LikeProductByUser(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(null as Product);

            _mockUow.Setup(uow => uow.ConfirmAsync()).ReturnsAsync(false);

            var result = await _likeController.LikeProduct(It.IsAny<int>());
            var objResult = (result.Result as ObjectResult);

            objResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objResult.Value.Should().Be("Unable to like the product");
        }

        [Fact]
        public async Task UnlikeProduct_ShouldReturnOk_WhenAbleToUnlikeProduct()
        {
            _productRepository
                .Setup(p => p.UnlikeProductByUser(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Product());

            _mockUow.Setup(uow => uow.ConfirmAsync()).ReturnsAsync(true);

            var result = await _likeController.UnlikeProduct(It.IsAny<int>());
            var objResult = (result.Result as ObjectResult);

            objResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            objResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task UnikeProduct_ShouldReturnBadRequest_WhenUnableToUnlikeProduct()
        {
            _productRepository
                .Setup(p => p.UnlikeProductByUser(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(null as Product);

            _mockUow.Setup(uow => uow.ConfirmAsync()).ReturnsAsync(false);

            var result = await _likeController.UnlikeProduct(It.IsAny<int>());
            var objResult = (result.Result as ObjectResult);

            objResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objResult.Value.Should().Be("Unable to unlike the product");
        }
    }
}
