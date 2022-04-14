using FluentAssertions;
using IdentityServer.Controllers;
using System;
using Xunit;
using Moq;
using AutoMapper;
using IdentityServer.Services.Interfaces;
using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using IdentityServer.DTO;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Linq;

namespace IdentityServerTests
{
    public class UserControllerTests
    {
        private UserController _userController;
        private Mock<UserManager<AppUser>> _userManager;

        //private List<UserRegisterDto> GetTestUsers()
        //{
        //    var users = new List<UserRegisterDto>
        //    {
        //        new UserRegisterDto { UserName = "Tom", Password = "dddddd"},
        //        new UserRegisterDto { UserName = "Tom", Password = "dddddd"},
        //        new UserRegisterDto { UserName = "Tom", Password = "dddddd"},
        //        new UserRegisterDto { UserName = "Tom", Password = "dddddd"},
        //        new UserRegisterDto { UserName = "Tom", Password = "dddddd"},
        //        new UserRegisterDto { UserName = "Tom", Password = "dddddd"}
        //    };
        //    return users;
        //}

        public UserControllerTests()
        {
            var mapper = new Mock<IMapper>();
            var tokenService = new Mock<ITokenService>();
            _userManager = new Mock<UserManager<AppUser>>();

            _userManager.CallBase = true;
           

            var signInManager = new Mock<SignInManager<AppUser>>();
            var roleManager = new Mock<RoleManager<AppRole>>();

            //var roleManager = new RoleManager<AppRole>();
            _userController = new UserController(
                mapper.Object,
                tokenService.Object,
                _userManager.Object,
                signInManager.Object,
                roleManager.Object
            );


        }

        [Fact]
        public void UserIsAlreadyExists()
        {
       

            //arrange
            var userDto = new UserRegisterDto { UserName = "Tomass", Password = "dd800Z" };
            

            //act
            var result = _userController.RegisterUser(userDto);
            _userManager.Setup(x => x.Users.FirstOrDefault()).Returns(null);
            _userController.
            //assert
            //Assert.IsType<OkResult>(result);
            //Assert.IsType<BadRequestObjectResult>(result);
        }

       

        //[Fact]
        //public void ShouldReturnTrue()
        //{
        //    //Assert.Equal<int>(0, 0);
        //    true.Should().BeTrue();
        //}
    }
}
