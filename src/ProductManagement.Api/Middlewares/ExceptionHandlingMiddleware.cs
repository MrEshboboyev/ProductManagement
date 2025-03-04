using FluentValidation;
using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = "error",
            message = exception.Message
        };

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var validationResponse = new
                {
                    status = "validation_error",
                    message = "One or more validation errors occurred",
                    errors = validationEx.Errors.Select(e => new
                    {
                        property = e.PropertyName,
                        message = e.ErrorMessage
                    })
                };
                await context.Response.WriteAsJsonAsync(validationResponse);
                return;

            case DomainException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            case ApplicationException:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        await context.Response.WriteAsJsonAsync(response);
    }
}