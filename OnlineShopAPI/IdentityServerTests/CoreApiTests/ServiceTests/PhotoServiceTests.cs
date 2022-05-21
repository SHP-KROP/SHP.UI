using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using OnlineShopAPI.Services;
using OnlineShopAPI.Services.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShopTests.CoreApiTests.ServiceTests
{
    public class PhotoServiceTests
    {
        private readonly IPhotoService _photoService;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IFormFile> _file;

        public PhotoServiceTests()
        {
            _photoService = new PhotoService(_uow.Object, _configuration.Object);
        }

        [Fact]
        public async Task AddPhotoToUser_ShouldReturnTrue_WhenPhotoAdded()
        {
            int id = 1;


        }
    }
}
