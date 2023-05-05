using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SHP.OnlineShopAPI.Web.Controllers;
using SHP.OnlineShopAPI.Web.Services.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace SHP.OnlineShopAPI.Tests.ControllerTests
{
    public class UserProfileControllerTests
    {
        private readonly UserProfileController _userProfileController;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IPhotoService> _photoService;
        private readonly Mock<IFormFile> _formFile;
        private readonly Mock<IUserRepository> _userRepository;

        public UserProfileControllerTests()
        {
            // Data access setup
            _uow = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _uow.SetupGet(uow => uow.UserRepository).Returns(_userRepository.Object);

            // Service setup
            _photoService = new Mock<IPhotoService>();

            // Data setup
            _formFile = new Mock<IFormFile>();

            _userProfileController = new UserProfileController(_uow.Object, _photoService.Object);
        }

        #region AddPhotoToUser

        [Fact]
        public async Task AddPhotoToUser_ShouldReturnOkWithPhotoUrl_WhenPhotoProvided()
        {
            _photoService
                .Setup(ps => ps.AddPhotoToUser(It.IsAny<int>(), It.IsAny<IFormFile>()))
                .ReturnsAsync(true);

            var user = new AppUser { PhotoUrl = "some url" };

            _userRepository
                .Setup(ur => ur.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(user);

            var result = await _userProfileController.AddPhotoToUser(_formFile.Object) as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().NotBeNull().And.Be(user.PhotoUrl);
        }

        [Fact]
        public async Task AddPhotoToUser_ShouldReturnBadRequest_WhenPhotoIsNull()
        {
            var result = await _userProfileController.AddPhotoToUser(null) as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
        #endregion

        #region AddPhotoToProduct

        [Fact]
        public async Task AddPhotoToProduct_ShouldReturnOk_WhenPhotoUploaded()
        {
            var user = new AppUser();

            _userRepository.Setup(ur => ur.GetUserWithProductsWithPhotosAsync(It.IsAny<int>())).ReturnsAsync(user);

            _photoService
                .Setup(ps => ps.AddPhotoToProduct(It.IsAny<AppUser>(), It.IsAny<int>(), It.IsAny<IFormFile>()))
                .ReturnsAsync(true);

            var result = await _userProfileController.AddPhotoToProduct(1, _formFile.Object) as StatusCodeResult;

            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task AddPhotoToProduct_ShouldReturnOk_WhenPhotoIsNull()
        {
            var user = new AppUser();

            _userRepository.Setup(ur => ur.GetUserWithProductsWithPhotosAsync(It.IsAny<int>())).ReturnsAsync(user);

            _photoService
                .Setup(ps => ps.AddPhotoToProduct(It.IsAny<AppUser>(), It.IsAny<int>(), It.IsAny<IFormFile>()))
                .ReturnsAsync(false);

            var result = await _userProfileController.AddPhotoToProduct(1, _formFile.Object) as ObjectResult;

            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        #endregion
    }
}
