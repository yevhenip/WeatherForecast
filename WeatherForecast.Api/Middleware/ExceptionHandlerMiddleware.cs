using System.Net;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Exceptions;

namespace WeatherForecast.Api.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            if (e is not DomainException domainException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "Unexpected error"
                });
                return;
            }

            var statusCode = domainException switch
            {
                EntityNotFoundException => HttpStatusCode.NotFound,
                BehaviourValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)statusCode,
                Title = domainException.Code,
                Detail = domainException.Message,
                Extensions = domainException.Parameters
            });
        }
    }
}