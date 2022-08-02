using DAL;
using IdentityServer.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var build = CreateHostBuilder(args).Build();

            using (var scope = build.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<OnlineShopContext>();
                context.Database.EnsureCreated();

                var seeder = services.GetRequiredService<Seeder>();

                await seeder.Seed();
            }

            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
