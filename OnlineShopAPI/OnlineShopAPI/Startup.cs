using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShopAPI.Extensions;
using OnlineShopAPI.Options;

namespace OnlineShopAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapping();
            services.ProvideIdentity();
            services.AddBearerAuthentication(Configuration);

            services.AddInjectableServices();

            services.AddDbContext<OnlineShopContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SHP.Data"));
            });

            services.AddNewtonsoftSupport();

            services.AddCors(o =>
            {
                o.AddPolicy(name: Configuration[ConfigurationOptions.CorsPolicyName], p =>
                {
                    p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            services.AddControllers();
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShopAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(Configuration[ConfigurationOptions.CorsPolicyName]);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
