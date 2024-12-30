using MediatR;
using FluentResults;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Application.Responses;
using vsa_journey.Features.Authentication.Login.Commands;
using vsa_journey.Features.Authentication.Logout.Command;
using vsa_journey.Features.Authentication.Signup.Commands;
using vsa_journey.Features.Authentication.RefreshToken.Command;
using vsa_journey.Features.Authentication.VerifyAccount.Commands;

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
    public async Task<IActionResult> Login(LoginCommand command)
    {
        return await HandleMediatorResult(_mediator.Send(command));
    }
    
    
    [HttpPost]
    [Route("Refresh")]
    public async Task<IActionResult> RefreshToken(RefreshAccessTokenCommand command)
    {
        
        return await HandleMediatorResult(_mediator.Send(command));
    }


    [HttpPost]
    [Route("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        return await HandleMediatorResult(_mediator.Send(command));
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

 



    private async Task<IActionResult> HandleMediatorResult<T>(Task<Result<T>> task)
    {
        var result = await task;
    
        if (result.IsSuccess)
        {
            var successMessage = result.Successes.FirstOrDefault()?.Message ?? "Operation successful";
            var data = result.ValueOrDefault;

            return Ok(_apiResponse.Success(message: successMessage, data: data));
        }

        var requestId = HttpContext.TraceIdentifier;
        var errors = result.Errors.Select(error => error.Message);
        var requestPath = HttpContext.Request.Path;

        return BadRequest(_apiResponse.BadRequest(requestId, errors, requestPath));
    }

    
}