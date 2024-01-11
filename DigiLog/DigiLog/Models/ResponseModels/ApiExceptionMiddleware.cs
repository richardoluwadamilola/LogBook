using Newtonsoft.Json;
using System.Net;

namespace DigiLog.Models.ResponseModels
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext, IWebHostEnvironment env)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex, env);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = JsonConvert.SerializeObject(new
            {
                StatusCode = context.Response.StatusCode,
                Message = env.IsDevelopment() ? exception.Message : "Internal Server Error"
            });

            await context.Response.WriteAsync(response);
            
        }
    }
}
