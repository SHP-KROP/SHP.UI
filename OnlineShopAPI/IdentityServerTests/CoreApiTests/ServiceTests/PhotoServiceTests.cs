using CloudinaryDotNet.Actions;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using OnlineShopAPI.Services;
using OnlineShopAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopTests.CoreApiTests.ServiceTests
{
    public class PhotoServiceTests
    {
        private readonly IPhotoService _photoService;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<ICloudinaryService> _cloudinaryService;
        private readonly Mock<IFormFile> _file;
        private readonly Mock<IUserRepository> _userRepository;
        public PhotoServiceTests()
        {
            _uow = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _uow.SetupGet(uow => uow.UserRepository).Returns(_userRepository.Object);

            _cloudinaryService = new Mock<ICloudinaryService>();
            _file = new Mock<IFormFile>();

            _photoService = new PhotoService(_uow.Object, _cloudinaryService.Object);
        }

        #region AddPhotoToUser
        [Fact]
        public async Task AddPhotoToUser_ShouldReturnTrue_WhenPhotoAdded()
        {
            int id = 1;
            var imageUploadResult = new ImageUploadResult { Url = new Uri("http://someurl") };

            _cloudinaryService
                .Setup(cs => cs.UploadAsync(It.IsAny<ImageUploadParams>()))
                .ReturnsAsync(imageUploadResult);

            _userRepository.Setup(ur => ur.FindAsync(It.IsAny<int>())).ReturnsAsync(new AppUser());

            var result = await _photoService.AddPhotoToUser(id, _file.Object);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task AddPhotoToUser_ShouldReturnFalse_WhenUserNotFound()
        {
            int id = 1;

            _userRepository.Setup(ur => ur.FindAsync(It.IsAny<int>())).ReturnsAsync(null as AppUser);

            var result = await _photoService.AddPhotoToUser(id, _file.Object);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task AddPhotoToUser_ShouldReturnFalse_WhenPhotoIsNull()
        {
            int id = 1;

            var result = await _photoService.AddPhotoToUser(id, null);

            result.Should().BeFalse();
        }

        #endregion

        #region AddPhotoToProduct

        [Fact]
        public async Task AddPhotoToProduct_ShouldReturnFalse_WhenPhotoIsNull()
        {
            var result = await _photoService.AddPhotoToProduct(new AppUser(), 1, null);

            result.Should().BeFalse();
        }

        #endregion
    }
}
