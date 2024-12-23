namespace vsa_journey.Application.Responses;

public class ApiResponse
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

    public object Created(object? data = null, string? message = "Resouce created successfully")
    {
        return BaseResponse((int)StatusCodes.Status201Created, message, data);
    }
    public object Success(object? data = null, string message = "Operation successful")
    {
        return BaseResponse((int)StatusCodes.Status200OK, message, data);
    }
}