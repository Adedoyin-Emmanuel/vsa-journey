using FluentValidation;
using vsa_journey.Application.Responses;
using vsa_journey.Utils;

namespace vsa_journey.Infrastructure.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly IApiResponse _apiResponse;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger, IApiResponse apiResponse)
    {
        _logger = logger;
        _apiResponse = apiResponse;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
       
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var requestId = context.TraceIdentifier;
        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = (int) StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(_apiResponse.BadRequest(
                    requestId,
                    errors: validationException.Errors.Select(e => new
                    {
                        name = e.PropertyName,
                        message = e.ErrorMessage
                    }),
                    message: "Validation exception has occurred."
                    ));
                break;
            
            
            default:
                context.Response.StatusCode = (int) StatusCodes.Status500InternalServerError;
                var error = exception.Message;
                await context.Response.WriteAsJsonAsync(_apiResponse.InternalServerError(
                    requestId,
                    errors: null,
                    message: EnvConfig.IsProduction ? "An unknown error occurred" : error
                ));

                break;
        }
        
    }
}