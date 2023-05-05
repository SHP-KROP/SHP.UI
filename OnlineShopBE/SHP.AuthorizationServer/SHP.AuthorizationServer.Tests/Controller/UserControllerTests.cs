using AutoMapper;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using SHP.AuthorizationServer.Tests.Helpers;
using SHP.AuthorizationServer.Web.Contracts;
using SHP.AuthorizationServer.Web.Controllers;
using SHP.AuthorizationServer.Web.DTO;
using SHP.AuthorizationServer.Web.DTO.Auth.Google;
using SHP.AuthorizationServer.Web.Services.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace SHP.AuthorizationServer.Tests.Controller
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITokenService> _tokenService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ISignInManager> _mockSignInManager;

        public UserControllerTests()
        {
            _mapper = new Mock<IMapper>();
            _tokenService = new Mock<ITokenService>();
            _tokenService.Setup(t => t.CreateToken(
                It.IsAny<AppUser>(), It.IsAny<ICollection<string>>(), It.IsAny<RefreshToken>()))
                .ReturnsAsync(new AuthenticationResult { Success = true });

            _uow = new Mock<IUnitOfWork>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockSignInManager = new Mock<ISignInManager>();

            _uow.SetupGet(u => u.UserRepository).Returns(_mockUserRepository.Object);
            _uow.SetupGet(u => u.SignInManager).Returns(_mockSignInManager.Object);


            _userController = new UserController(
                _mapper.Object,
                _tokenService.Object,
                _uow.Object,
                new Mock<IAuthService<GoogleOAuthDto>>().Object
            );

            _tokenService.Setup(ut => ut.CreateToken(It.IsAny<AppUser>(), new List<string>(), null))
                .ReturnsAsync(new AuthenticationResult { Success = true });
        }

        [Fact]
        public async void LogIn_ShouldReturnUnathorized_WhenPasswordIsNull()
        {
            var userDto = new UserLogInDto { UserName = "Tomass", Password = null };

            var signInResult = new SignInResult();

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync
                (new AppUser { UserName = "Tomass" });

            _mockSignInManager
                .Setup(m => m.CheckPasswordSignInAsync
                (
                    It.IsAny<AppUser>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()
                )).ReturnsAsync(signInResult);

            _mapper
                .Setup(m => m.Map<UserDto>(It.IsAny<AppUser>()))
                .Returns(new UserDto { UserName = "Tomass" });

            var response = await _userController.LogIn(userDto);

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse.Value.Should().BeNull();
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async void LogIn_ShouldReturnOk_WhenSignInResultSucceeded()
        {
            var userDto = new UserLogInDto { UserName = "Tomass", Password = "correctPass" };

            var successfulSignInResult = new SignInResult();

            successfulSignInResult.GetType().GetProperty("Succeeded").SetValue(successfulSignInResult, true);

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync
                (new AppUser { UserName = "Tomass" });

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

            var successfulSignInResult = new SignInResult();

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
        public async void RegisterUser_ShouldReturnUnauthorized_WhenUserAlreadyExists()
        {
            var userDto = new UserRegisterDto { UserName = "Tomass", Password = "dd800Z" };

            _mockUserRepository
                .Setup(ur => ur.GetUserByUsernameAsync(It.IsAny<string>()))
                .ReturnsAsync
                (new AppUser
                {
                    UserName = "asd"
                }
                );

            var response = await _userController.RegisterUser(userDto);

            var parsedResponse = new Response<UserDto>(response);

            parsedResponse.Value.Should().BeNull();
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async void RegisterUser_ShouldReturnOk_WhenRegistrationResultSucceeded()
        {
            var userDto = new UserRegisterDto { UserName = "ADDDS7", Password = "ZalpO08" };

            var identityResult = new IdentityResult();

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
        public async void RegisterUser_ShouldReturnUnauthorized_WhenUserCannotBeRegistered()
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
            parsedResponse.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }
    }
}


