using MediatR;
using FluentResults;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Domain.Constants;
using vsa_journey.Application.Responses;
using vsa_journey.Features.Authentication.Commands.Signup;
using vsa_journey.Features.Authentication.Commands.VerifyAccount;

namespace vsa_journey.Features.Authentication.Controller;


[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;
    private readonly IApiResponse _apiResponse;
    


    public AuthController(IMediator mediator, ILogger<AuthController> logger, IApiResponse apiResponse)
    {
        _mediator = mediator;
        _logger = logger;
        _apiResponse = apiResponse;
    }


    [HttpPost]
    [Route("Signup")]
    public async Task<IActionResult> Signup(SignupCommand command)
    {
        return await HandleMediatorResult(_mediator.Send(command));
    }
    
    [HttpPost]
    [Route("Verify")]
    public async Task<IActionResult> Verify(VerifyAccountCommand command)
    {
        return await HandleMediatorResult(_mediator.Send(command));
    }


    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }


    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }

    [HttpPost]
    [Route("Forgot-Password")]
    public async Task<IActionResult> ForgotPassword()
    {
        return Ok();
    }

    [HttpPost]
    [Route("Reset-Password")]
    public async Task<IActionResult> ResetPassword()
    {
        return Ok();
    }

    [HttpGet]
    [Route("Refresh-Token")]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok();
    }



    private async Task<IActionResult> HandleMediatorResult(Task<Result> task)
    {
        var result = await task;
    
        if (result.IsSuccess)
        {
            var successMessage = result.Successes.FirstOrDefault()?.Message ?? "Operation successful";

            var data = result.Successes
                .FirstOrDefault()?
                .Metadata?.ContainsKey("Data") == true
                ? result.Successes.FirstOrDefault()?.Metadata["Data"]
                : null;

            return Ok(_apiResponse.Success(message: successMessage, data: data));
        }

        var requestId = HttpContext.TraceIdentifier;
        var errors = result.Errors.Select(error => new
        {
            Name = error.Metadata.ContainsKey("Name") ? error.Metadata["Name"] : "Unknown",
            Message = error.Message
        });
        var requestPath = HttpContext.Request.Path;

        return BadRequest(_apiResponse.BadRequest(requestId, errors, requestPath));
    }

    
}