using ProductManagementAPI.Exceptions;
using ProductManagementAPI.Models;
using System.Text.Json;

namespace ProductManagementAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private ILogger<ExceptionMiddleware> _logger;
    private IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, 
        ILogger<ExceptionMiddleware> 
        logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                NotFoundException => context.Response.StatusCode = StatusCodes.Status404NotFound,
                BadRequestException => context.Response.StatusCode = StatusCodes.Status400BadRequest,
                BussinesException => context.Response.StatusCode = StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
                
            };

            var response = _env.IsDevelopment() ?
                new ErrorDetails(statusCode, ex.Message, ex.StackTrace) :
                new ErrorDetails(statusCode, "...", "Internal Server Error");

            var json = JsonSerializer.Serialize(response);
             
            await context.Response.WriteAsync(json);
        }
    }
}
