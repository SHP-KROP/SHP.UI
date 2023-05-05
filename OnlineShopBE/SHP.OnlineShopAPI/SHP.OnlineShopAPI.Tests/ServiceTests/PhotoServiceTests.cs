using CloudinaryDotNet.Actions;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using SHP.OnlineShopAPI.Web.Services;
using SHP.OnlineShopAPI.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SHP.OnlineShopAPI.Tests.ServiceTests
{
    public class PhotoServiceTests
    {
        private readonly IPhotoService _photoService;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<ICloudinaryService> _cloudinaryService;
        private readonly Mock<IFormFile> _file;
        private readonly Mock<IUserRepository> _userRepository;

        private int _userId = 1;
        private int _productId = 1;
        private int _invalidProductId = -1;

        public PhotoServiceTests()
        {
            _uow = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _uow.SetupGet(uow => uow.UserRepository).Returns(_userRepository.Object);

            _cloudinaryService = new Mock<ICloudinaryService>();
            _file = new Mock<IFormFile>();

            _photoService = new PhotoService(_uow.Object, _cloudinaryService.Object);
        }

        private AppUser GetValidUser() => new AppUser
        {
            Id = _productId,
            Products = new List<Product>
            {
                new Product
                {
                    Id = _productId,
                    Photos = new List<Photo>()
                }
            }
        };

        #region AddPhotoToUser
        [Fact]
        public async Task AddPhotoToUser_ShouldReturnTrue_WhenPhotoAdded()
        {
            var imageUploadResult = new ImageUploadResult { Url = new Uri("http://someurl") };

            _cloudinaryService
                .Setup(cs => cs.UploadAsync(It.IsAny<ImageUploadParams>()))
                .ReturnsAsync(imageUploadResult);

            _userRepository.Setup(ur => ur.FindAsync(It.IsAny<int>())).ReturnsAsync(new AppUser());

            var result = await _photoService.AddPhotoToUser(_userId, _file.Object);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddPhotoToUser_ShouldReturnFalse_WhenUserNotFound()
        {
            _userRepository.Setup(ur => ur.FindAsync(It.IsAny<int>())).ReturnsAsync(null as AppUser);

            var result = await _photoService.AddPhotoToUser(_userId, _file.Object);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddPhotoToUser_ShouldReturnFalse_WhenPhotoIsNull()
        {
            var result = await _photoService.AddPhotoToUser(_userId, null);

            result.Should().BeFalse();
        }

        #endregion

        #region AddPhotoToProduct

        [Fact]
        public async Task AddPhotoToProduct_ShouldReturnFalse_WhenPhotoIsNull()
        {
            var result = await _photoService.AddPhotoToProduct(GetValidUser(), _productId, null);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddPhotoToProduct_ShouldReturnTrue_WhenPhotoAdded()
        {
            var imageUploadResult = new ImageUploadResult { Url = new Uri("http://someurl") };

            _cloudinaryService
                .Setup(cs => cs.UploadAsync(It.IsAny<ImageUploadParams>()))
                .ReturnsAsync(imageUploadResult);

            _userRepository.Setup(ur => ur.FindAsync(It.IsAny<int>())).ReturnsAsync(GetValidUser());

            var result = await _photoService.AddPhotoToProduct(GetValidUser(), _productId, _file.Object);
            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddPhotoToProduct_ShouldReturnFalse_WhenProductNotFound()
        {
            var imageUploadResult = new ImageUploadResult { Url = new Uri("http://someurl") };

            _cloudinaryService
                .Setup(cs => cs.UploadAsync(It.IsAny<ImageUploadParams>()))
                .ReturnsAsync(imageUploadResult);

            _userRepository.Setup(ur => ur.FindAsync(It.IsAny<int>())).ReturnsAsync(GetValidUser());

            var result = await _photoService.AddPhotoToProduct(GetValidUser(), _invalidProductId, _file.Object);
            result.Should().BeFalse();
        }

        #endregion
    }
}
