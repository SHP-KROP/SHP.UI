using DAL;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using IdentityServer;
using IdentityServer.Helpers;
using IdentityServer.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.IntegrationTests.IdentityServer
{
    public class ModeratorControllerTests
    {
        private const string DefaultRoute = "/api/moderator";
        private CustomWebApplicationFactory<Program> _webFactory;
        private HttpClient _httpClient;

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

        [Fact]
        public async void Get_ShouldReturnUnauthorized_WhenUserWithoutToken()
        {
            var response = await _httpClient.GetAsync(DefaultRoute);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async void Get_ShouldReturnOk_WhenUsersFound()
        {
            AddBearerToken();
            ClearDb();
            await PrepareDb();

            var response = await _httpClient.GetAsync(DefaultRoute);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void Get_ShouldReturnNoContent_WhenUsersNotFound()
        {
            AddBearerToken();
            ClearDb();

            var response = await _httpClient.GetAsync(DefaultRoute);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        private void AddBearerToken()
        {
            var config = _webFactory.Services.GetRequiredService<IConfiguration>();
            var token = new TokenService(config).CreateToken(User, new[] { Roles.Moder });

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
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
    }
}
