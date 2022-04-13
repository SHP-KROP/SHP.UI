using FluentAssertions;
using OnlineShopAPI.Controllers;
using Xunit;

namespace OnlineShopAPI.Tests
{
    public class CategoryControllerTests
    {
        private ProductController _productController;

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
