using DAL;
using DAL.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SHP.AuthorizationServer.Web;
using SHP.AuthorizationServer.Web.Helpers;
using SHP.AuthorizationServer.Web.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SHP.AuthorizationServer.IntegrationTests.Controller
{
    public class ModeratorControllerTests
    {
        private const string DefaultRoute = "/api/moderator";
        private readonly CustomWebApplicationFactory<Program> _webFactory;
        private readonly HttpClient _httpClient;

        public ModeratorControllerTests()
        {
            _webFactory = new CustomWebApplicationFactory<Program>();
            _httpClient = _webFactory.CreateDefaultClient();
        }

        private AppUser User => new AppUser
        {
            Id = 7000,
            UserName = "Denis",
            Email = "some@mail.com"
        };

        #region Get

        [Fact]
        public async void Get_ShouldReturnUnauthorized_WhenUserWithoutToken()
        {
            var response = await _httpClient.GetAsync(DefaultRoute);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async void Get_ShouldReturnOk_WhenUsersFound()
        {
            await AddBearerToken();
            ClearDb();
            await PrepareDb();

            var response = await _httpClient.GetAsync(DefaultRoute);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void Get_ShouldReturnNoContent_WhenUsersNotFound()
        {
            await AddBearerToken();
            ClearDb();

            var response = await _httpClient.GetAsync(DefaultRoute);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        #endregion

        #region Delete

        [Fact]
        public async void Delete_ShouldReturnNoContent_WhenUserNotFound()
        {
            await AddBearerToken();
            await PrepareDb();

            var response = await _httpClient.DeleteAsync($"{DefaultRoute}/-1");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void Delete_ShouldReturnBadRequest_WhenUserHasSiteManagingRole(int id)
        {
            await AddBearerToken();
            await PrepareDb();

            var response = await _httpClient.DeleteAsync($"{DefaultRoute}/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public async void Delete_ShouldReturnOk_WhenUserSuccessfullyDeleted(int id)
        {
            await AddBearerToken();
            await PrepareDb();

            var response = await _httpClient.DeleteAsync($"{DefaultRoute}/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        #endregion

        #region Helper methods

        private async Task AddBearerToken()
        {
            var scope = _webFactory.Services.CreateScope();
            var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            var authResult = await tokenService.CreateToken(User, new[] { Roles.Moder }, null);

            if (!authResult.Success)
            {
                return;
            }

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authResult.Token}");
        }

        private async Task PrepareDb()
        {
            var serviceFactory = _webFactory.Services.GetRequiredService<IServiceScopeFactory>();

            using (var scope = serviceFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeder = services.GetRequiredService<Seeder>();
                await seeder.Seed();
            }
        }

        private void ClearDb()
        {
            var serviceFactory = _webFactory.Services.GetRequiredService<IServiceScopeFactory>();

            using (var scope = serviceFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<OnlineShopContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.SaveChanges();
            }
        }

        #endregion

    }
}
