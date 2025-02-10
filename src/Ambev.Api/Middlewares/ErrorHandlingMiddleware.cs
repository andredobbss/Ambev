using Ambev.Application.Shared;
using Ambev.Domain.Resourcers;
using Ambev.Domain.Validations;
using System.Net;
using System.Text.Json;

namespace Ambev.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(context, ex);
        }
    }


    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Console.WriteLine($"Erro capturado no middleware: {exception.GetType().Name} - {exception.Message}");
        Console.WriteLine(exception.StackTrace);


        context.Response.ContentType = "application/json";

        ApiErrorResponse response;

        if (exception is DomainValidationException domainException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = domainException.Errors
                .Select(e => new { Field = e.PropertyName, Message = e.ErrorMessage })
                .ToList();

            response = new ApiErrorResponse(
                type: ResourceMessagesException.ERROR_TYPE_DOMAIN,
                error: ResourceMessagesException.ERROR_DOMAIN,
                detail: ResourceMessagesException.ERROR_DETAIL_DOMAIN,
                errors: errors
            );
        }
        else if (exception is KeyNotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            response = new ApiErrorResponse(ResourceMessagesException.ERROR_TYPE_RESOURCE_NOT_FOUND, ResourceMessagesException.ERROR_RESOURCE_NOT_FOUND, exception.Message);
        }
        else if (exception is UnauthorizedAccessException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            response = new ApiErrorResponse(ResourceMessagesException.ERROR_TYPE_AUTHENTICATION, ResourceMessagesException.ERROR_AUTHENTICATION, ResourceMessagesException.ERROR_DETAIL_AUTHENTICATION);
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response = new ApiErrorResponse(ResourceMessagesException.ERROR_TYPE_INTERNAL_SERVER, ResourceMessagesException.ERROR_INTERNAL_SERVER, ResourceMessagesException.ERROR_DETAIL_INTERNAL_SERVER);
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));

    }
}