using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DigiLog.Models.ResponseModels
{
    // Middleware for handling exceptions in the API and providing a consistent error response.
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;

        // Constructor to initialize the middleware with the next middleware in the pipeline and a logger.
        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Middleware execution method, catches exceptions and handles them.
        public async Task Invoke(HttpContext httpContext, IWebHostEnvironment env)
        {
            try
            {
                // Invoke the next middleware in the pipeline.
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Log the unexpected exception.
                _logger.LogError($"An unexpected error occurred: {ex}");

                // Handle the exception and send a standardized error response.
                HandleException(httpContext, ex, env);
            }
        }

        // Method to handle exceptions and send a standardized error response.
        private void HandleException(HttpContext context, Exception exception, IWebHostEnvironment env)
        {
            // Set the response content type to JSON.
            context.Response.ContentType = "application/json";

            // Set the HTTP status code to Internal Server Error.
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Create a JSON representation of the error response.
            var response = JsonConvert.SerializeObject(new
            {
                StatusCode = context.Response.StatusCode,
                Message = env.IsDevelopment() ? exception.Message : "Internal Server Error"
            });

            // Log the details of the exception.
            _logger.LogError($"Exception details: {exception}");

            // Send the error response to the client.
            context.Response.WriteAsync(response);
        }
    }
}
