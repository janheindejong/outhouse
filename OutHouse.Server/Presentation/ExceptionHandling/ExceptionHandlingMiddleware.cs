using OutHouse.Server.Domain.Exceptions;
using System.Text.Json;

namespace OutHouse.Server.Presentation.ExceptionHandling
{
    internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (OuthouseException e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception switch
            {
                SeeOtherException => StatusCodes.Status303SeeOther,
                BadRequestException => StatusCodes.Status400BadRequest,
                ForbiddenException => StatusCodes.Status403Forbidden,
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                _ => throw exception
            };
            var response = new
            {
                error = exception.Message
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
