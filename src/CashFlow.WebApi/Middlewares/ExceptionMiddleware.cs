using System.Net.Mime;
using CashFlow.Domain.Exceptions;
using CashFlow.IoC.Factories;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

namespace CashFlow.Api.Middlewares;

public static class ExceptionMiddleware
{
    private const string GenericMessage = "An error has occurred.";

    public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
    {
        app
            .UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(async context =>
            {
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                context.Response.StatusCode = exception == null
                    ? StatusCodes.Status500InternalServerError
                    : GetStatusCodeByExceptionType(exception);

                var message = context.Response.StatusCode == StatusCodes.Status500InternalServerError
                    ? GenericMessage
                    : exception!.Message;

                ContainerFactory.GetInstance<IDiagnosticContext>()?.SetException(exception);

                await context.Response.WriteAsync(message);
            }));
    }

    private static int GetStatusCodeByExceptionType(Exception exception)
    {
        return exception switch
        {
            CashFlowNotFoundException => StatusCodes.Status404NotFound,
            CashFlowInvalidOperationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}