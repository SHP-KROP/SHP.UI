using FluentAssertions;
using OnlineShopAPI.Controllers;
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
