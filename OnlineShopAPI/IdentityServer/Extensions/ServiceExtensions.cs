using AutoMapper;
using DAL;
using DAL.Entities;
using IdentityServer.DTO.Google;
using IdentityServer.Helpers;
using IdentityServer.Mapping;
using IdentityServer.Options;
using IdentityServer.Services;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

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

        public static IServiceCollection AddBearerAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[ConfigurationOptions.Token])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization using Bearer scheme 'Bearer <token>'",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}
