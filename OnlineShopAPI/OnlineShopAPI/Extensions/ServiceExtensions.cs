using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopAPI.Mapping;

namespace OnlineShopAPI.Extensions
{
    public static class ServiceExtensions
    {
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
