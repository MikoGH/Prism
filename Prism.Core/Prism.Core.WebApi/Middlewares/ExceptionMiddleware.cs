using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Prism.Core.WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext, "httpContext");
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception ex, HttpStatusCode statusCode)
    {
        httpContext.Items.TryAdd("LoggedException", ex);
        _logger.LogError(new EventId(ex.HResult), ex, ex.ToString());
        return WriteJsonContentAsync(httpContext, statusCode, ex.Message);
    }

    private static Task WriteJsonContentAsync<TError>(HttpContext httpContext, HttpStatusCode statusCode, TError error)
    {
        httpContext.Response.StatusCode = (int)statusCode;
        return httpContext.Response.WriteAsJsonAsync(error);
    }
}
