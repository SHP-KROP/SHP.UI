using AutoMapper;
using FluentAssertions;
using IdentityServer.Controllers;
using IdentityServer.Data.Entities;
using IdentityServer.Data.Interfaces;
using IdentityServer.DTO;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IdentityServerTests
{
    public class UserControllerTests
    {
        private UserController _userController;
        private Mock<UserManager<AppUser>> _userManager;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISignInManager> _mockSignInManager;

        public UserControllerTests()
        {
            var mapper = new Mock<IMapper>();
            var tokenService = new Mock<ITokenService>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockSignInManager = new Mock<ISignInManager>();

            _userController = new UserController(
                mapper.Object,
                tokenService.Object,
                _mockUserRepository.Object,
                _mockSignInManager.Object
            );
        }

        [Fact]
        public async void RegisterUser_ShouldReturnBadRequest_WhenUserAlreadyExists()
        {
            var userDto = new UserRegisterDto { UserName = "Tomass", Password = "dd800Z" };

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync
                ( new AppUser
                    {
                        UserName = "asd"
                    }
                );

            var response = await _userController.RegisterUser(userDto);

            var result = response.Result as ObjectResult;
            var value = result?.Value as UserDto;

            value.Should().BeNull();
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
