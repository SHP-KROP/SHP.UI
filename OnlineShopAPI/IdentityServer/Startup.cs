using DAL;
using DAL.Interfaces;
using IdentityServer.Extensions;
using IdentityServer.Middlewares;
using IdentityServer.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
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
            services.AddDbContext<OnlineShopContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("SHP.Data"));
            });

            services.AddCors(o =>
            {
                o.AddPolicy(name: Configuration[ConfigurationOptions.CorsPolicyName], p =>
                {
                    p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.ProvideIdentity();
            services.AddAutoMapping();
            services.AddControllers();
            services.AddOAuthServices();

            services.AddBearerAuthentication(Configuration);

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ValidationHandlerMiddleware>(env);

            app.UseCors(Configuration[ConfigurationOptions.CorsPolicyName]);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
