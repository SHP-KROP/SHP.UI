using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineShopAPI.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopAPI.Tests
{
    public class CategoryControllerTests
    {
        private readonly CategoryController _categoryController;

        public CategoryControllerTests()
        {

        }

        [Fact]
        public void Test()
        {
            true.Should().BeTrue();
        }
    }
}
