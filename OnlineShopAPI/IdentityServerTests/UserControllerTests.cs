using AutoMapper;
using FluentAssertions;
using IdentityServer.Controllers;
using IdentityServer.Data.Entities;
using IdentityServer.Data.Interfaces;
using IdentityServer.DTO;
using IdentityServer.Services.Interfaces;
using IdentityServerTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Xunit;

namespace IdentityServerTests
{
    public class UserControllerTests
    {
        private UserController _userController;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISignInManager> _mockSignInManager;
        private readonly Mock<IMapper> _mapper;

        public UserControllerTests()
        {
            _mapper = new Mock<IMapper>();
            var tokenService = new Mock<ITokenService>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockSignInManager = new Mock<ISignInManager>();

            

            _userController = new UserController(
                _mapper.Object,
                tokenService.Object,
                _mockUserRepository.Object,
                _mockSignInManager.Object
            );
        }

        [Fact]
        public async void LogIn_ShouldReturnOk_WhenSignInResultSucceeded()
        {
            var userDto = new UserLogInDto { UserName = "Tomass", Password = "correctPass" };

            var successfulSignInResult = new Microsoft.AspNetCore.Identity.SignInResult();

            successfulSignInResult.GetType().GetProperty("Succeeded").SetValue(successfulSignInResult, true);

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync
                (new AppUser { UserName = "Tomass"});

            _mockSignInManager
                .Setup(m => m.CheckPasswordSignInAsync
                (
                    It.IsAny<AppUser>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()
                )).ReturnsAsync(successfulSignInResult);

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<AppUser>()))
                .Returns(new UserDto { UserName = "Tomass" });

            var response = await _userController.LogIn(userDto);

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse?.Value?.UserName.Should().BeSameAs(userDto.UserName);
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void LogIn_ShouldReturnUnauthorized_WhenUserPasswordIsWrong()
        {
            var userDto = new UserLogInDto { UserName = "Tomass", Password = "dd800Z" };

            var successfulSignInResult = new Microsoft.AspNetCore.Identity.SignInResult();

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync
                (new AppUser { UserName = "asd" });
            _mockSignInManager.Setup(
                m => m.CheckPasswordSignInAsync
                (
                    It.IsAny<AppUser>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()
                )).ReturnsAsync(successfulSignInResult);

            var response = await _userController.LogIn(userDto);

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse.Value.Should().BeNull();
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async void LogIn_ShouldReturnUnauthorized_WhenUserDoesNotExist()
        {
            var userDto = new UserLogInDto { UserName = "asd", Password = "Pa$$w0rd", };

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync(null as AppUser);

            var response = await _userController.LogIn(userDto);

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse.Value.Should().BeNull();
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
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

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse.Value.Should().BeNull();
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
