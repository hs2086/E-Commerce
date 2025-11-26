using Contracts.Logger;
using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace E_Commerce.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        FluentValidation.ValidationException => StatusCodes.Status422UnprocessableEntity,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    logger.LogError($"Something went wrong: {contextFeature.Error}");

                    ErrorDetails errorDetails = new ErrorDetails();
                    errorDetails.StatusCode = context.Response.StatusCode;

                    if (contextFeature.Error is FluentValidation.ValidationException validationException)
                    {
                        var errors = validationException.Errors.Select(e => new
                        {
                            Field = e.PropertyName,
                            Message = e.ErrorMessage
                        });
                        errorDetails.Message = errors;
                    }
                    else
                    {
                        errorDetails.Message = contextFeature.Error.Message;
                    }

                    await context.Response.WriteAsync(errorDetails.ToString());
                }
            });
        });
    }
}
