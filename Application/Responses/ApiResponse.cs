namespace vsa_journey.Application.Responses;

public class ApiResponse : IApiResponse

{
    private object BaseResponse(int code, string message, object? data = null)
    {
        return new
        {
            Code = code,
            Success = code is >= 200 and < 300,
            Message = message,
            Data = data,
        };
    }

    private object ValidationResponse(int code, string message, string requestId, string requestPath, object? errors = null)
    {
        return new
        {
            Code = code,
            Success = false,
            Message = message,
            RequestPath = requestPath,
            RequestId = requestId,
            Errors = errors,
        };
    }
    
    
    private object InternalServerErrorResponse(string message, string requestId, string requestPath, object? errors = null)
    {
        return new  
        {
            Code = (int) StatusCodes.Status500InternalServerError,
            Success = false,
            Message = message,
            RequestPath = requestPath,
            RequestId = requestId,
            Errors = errors,
        };
    }

    
    public object Created(object? data = null, string? message = "Resouce created successfully") => BaseResponse((int)StatusCodes.Status201Created, message, data);
    public object Success(object? data = null, string message = "Operation successful") => BaseResponse((int)StatusCodes.Status200OK, message, data);
    public object BadRequest( string requestId, object? errors, string requestPath, string message = "Invalid request" ) =>  ValidationResponse((int)StatusCodes.Status400BadRequest, message, requestId, requestPath, errors);
    public object NotFound(string message = "Resource not found") => BaseResponse((int)StatusCodes.Status404NotFound, message);
    public object Unauthorized(string message = "Unauthorized. Please login") => BaseResponse((int)StatusCodes.Status401Unauthorized, message);
    public object Forbidden(string message = "Forbidden. Insufficient rights") => BaseResponse((int)StatusCodes.Status403Forbidden, message);
    public object InternalServerError(string requestId, object ? errors, string requestPath, string message = "An unknown error occurred") => InternalServerErrorResponse(message, requestId, requestPath, errors);
}