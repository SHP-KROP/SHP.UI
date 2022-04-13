using FluentAssertions;
using IdentityServer.Controllers;
using System;
using Xunit;

namespace IdentityServerTests
{
    public class UserControllerTests
    {
        private readonly UserController _userController;

        public UserControllerTests()
        {

        }

        [Fact]
        public void ShouldReturnTrue()
        {
            true.Should().BeTrue();
        }
    }
}
