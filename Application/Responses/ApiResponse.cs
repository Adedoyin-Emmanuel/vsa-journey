namespace vsa_journey.Application.Responses;

public class ApiResponse : IApiResponse
{
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApiResponse(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    private object BaseResponse(int code, string message, object? data = null)
    {
        var context = _httpContextAccessor.HttpContext;
        
        return new
        {
            Code = code,
            Success = code is >= 200 and < 300,
            Message = message,
            Data = data,
            RequestId = context!.TraceIdentifier,
            RequestPath = context.Request.Path.Value,
        };
    }

    private object ValidationResponse(string? message = "Invalid request", object? errors = null)
    {
        var context = _httpContextAccessor.HttpContext;
        
        return new
        {
            Code = StatusCodes.Status400BadRequest,
            Success = false,
            Message = message,
            RequestId = context!.TraceIdentifier,
            RequestPath = context.Request.Path.Value,
            Errors = errors,
        };
    }
    
    
    private object InternalServerErrorResponse(string? message = "An unknown error occurred" , object? errors = null)
    {
        var context = _httpContextAccessor.HttpContext;
        
        return new  
        {
            Code = StatusCodes.Status500InternalServerError,
            Success = false,
            Message = message,
            RequestId = context!.TraceIdentifier,
            RequestPath = context.Request.Path.Value,
            Errors = errors,
        };
    }

    
    public object Created(object? data = null, string? message = "Resouce created successfully") => BaseResponse((int)StatusCodes.Status201Created, message, data);
    public object Ok(object? data = null, string message = "Operation successful") => BaseResponse((int)StatusCodes.Status200OK, message, data);
    public object BadRequest(object? errors, string? message = "Invalid request") =>  ValidationResponse(message, errors);
    public object NotFound(string message = "Resource not found") => BaseResponse((int)StatusCodes.Status404NotFound, message);
    public object Unauthorized(string message = "Unauthorized. Please login") => BaseResponse((int)StatusCodes.Status401Unauthorized, message);
    public object Forbidden(string message = "Forbidden. Insufficient rights") => BaseResponse((int)StatusCodes.Status403Forbidden, message);
    public object InternalServerError(object? errors, string? message = "An unknown error occurred") => InternalServerErrorResponse(message, errors);
}