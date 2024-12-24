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
    
    public object Created(object? data = null, string? message = "Resouce created successfully") => BaseResponse((int)StatusCodes.Status201Created, message, data);
    public object Success(object? data = null, string message = "Operation successful") => BaseResponse((int)StatusCodes.Status200OK, message, data);
    public object BadRequest(string message = "Invalid request") =>  BaseResponse((int)StatusCodes.Status400BadRequest, message);
    public object NotFound(string message = "Resource not found") => BaseResponse((int)StatusCodes.Status404NotFound, message);
    public object Unauthorized(string message = "Unauthorized. Please login") => BaseResponse((int)StatusCodes.Status401Unauthorized, message);
    public object Forbidden(string message = "Forbidden. Insufficient rights") => BaseResponse((int)StatusCodes.Status403Forbidden, message);
    public object InternalServerError(string message = "An unknown error occurred") => BaseResponse((int)StatusCodes.Status500InternalServerError, message);
}