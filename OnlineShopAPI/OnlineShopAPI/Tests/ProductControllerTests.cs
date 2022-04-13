using FluentAssertions;
using OnlineShopAPI.Controllers;
using Xunit;

namespace OnlineShopAPI.Tests
{
    public class ProductControllerTests
    {
        private ProductController _productController;

        public ProductControllerTests()
        {

        }

        [Fact]
        public void Test()
        {
            true.Should().BeTrue();
        }
    }
}
