using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SHP.AuthorizationServer.Web.Middlewares
{
    public class ValidationHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private const string InternalServerError = "Internal server error has occured";

        public ValidationHandlerMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = StatusCodes.Status500InternalServerError;

                string errorMessage = InternalServerError;

                if (_env.IsDevelopment())
                {
                    errorMessage += $" {error.Message}";
                }

                await response.WriteAsync(JsonSerializer.Serialize(errorMessage));
            }
        }
    }
}
