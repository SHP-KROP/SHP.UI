using AutoMapper;
using DAL;
using DAL.Entities;
using IdentityServer.DTO.Google;
using IdentityServer.Helpers;
using IdentityServer.Mapping;
using IdentityServer.Services;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ProvideIdentity(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<Seeder>();

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<OnlineShopContext>();

            services.AddAuthentication();

            return services;
        }

        public static IServiceCollection AddOAuthServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthService<GoogleOAuthDto>, GoogleAuthService>();

            return services;
        }

        public static IServiceCollection AddAutoMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper() as IMapper);

            return services;
        }
    }
}
