using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using IdentityServer.Controllers;
using IdentityServer.DTO;
using IdentityServer.Services.Interfaces;
using IdentityServerTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace IdentityServerTests
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITokenService> _tokenService;

        public UserControllerTests()
        {
            _mapper = new Mock<IMapper>();
            _tokenService = new Mock<ITokenService>();

            _uow = new Mock<IUnitOfWork>();

            _uow.SetupGet(_uow.Object.)

            _userController = new UserController(
                _mapper.Object,
                _tokenService.Object,
                _uow.Object
            );

            _tokenService.Setup(ut => ut.CreateToken(It.IsAny<AppUser>())).Returns("Great");
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

        [Fact]
        public async void RegisterUser_ShouldReturnOk_WhenRegistrationResultSucceeded()
        {
            var userDto = new UserRegisterDto { UserName = "ADDDS7", Password = "ZalpO08" };

            var identityResult = new Microsoft.AspNetCore.Identity.IdentityResult();

            identityResult.GetType().GetProperty("Succeeded").SetValue(identityResult, true);

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync(null as AppUser);

            _mockUserRepository
                .Setup(u => u.CreateUserAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);


            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<AppUser>()))
                .Returns(new UserDto { UserName = userDto.UserName });

            var response = await _userController.RegisterUser(userDto);

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse?.Value?.UserName.Should().BeSameAs(userDto.UserName);
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async void RegisterUser_ShouldReturnBadRequest_WhenUserCannotBeRegistered()
        {
            var userDto = new UserRegisterDto { UserName = "ADDDS7", Password = "Zalp" };

            var identityResult = new IdentityResult();

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync
                (null as AppUser);

            _mockUserRepository
                .Setup(u => u.CreateUserAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            var response = await _userController.RegisterUser(userDto);

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse.Value.Should().BeNull();
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}


