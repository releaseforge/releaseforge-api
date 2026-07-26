using System.Net;
using System.Text.Json;
using ReleaseForge.Application.Common.Exceptions;

namespace ReleaseForge.Api.Middleware;

internal sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        object response;

        if (exception is ValidationException validationEx)
        {
            statusCode = HttpStatusCode.BadRequest;
            response = new
            {
                StatusCode = (int)statusCode,
                Message = "Validation failed",
                validationEx.Errors
            };
        }
        else if (exception is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            response = new
            {
                StatusCode = (int)statusCode,
                Message = exception.Message
            };
        }
        else
        {
            response = new
            {
                StatusCode = (int)statusCode,
                Message = "An unexpected error occurred."
            };
        }

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception");
        }

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
