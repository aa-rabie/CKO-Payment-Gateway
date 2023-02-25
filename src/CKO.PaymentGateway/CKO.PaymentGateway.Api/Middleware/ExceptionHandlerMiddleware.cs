using CKO.PaymentGateway.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace CKO.PaymentGateway.Api.Middleware;

/// <summary>
/// Responsible for handling any unhandled exception and returns a proper response 
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAndGenerateResponse(context, ex);
        }
    }

    private static Task HandleExceptionAndGenerateResponse(HttpContext context, Exception exception)
    {
        HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case NotFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                break;
            case not null:
                httpStatusCode = HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;

        var result = JsonSerializer.Serialize(new { error = exception.Message });
        
        return context.Response.WriteAsync(result);
    }
}