using MediatR;
using FluentResults;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using vsa_journey.Features.Authentication.Login.Commands;
using vsa_journey.Features.Authentication.Logout.Command;
using vsa_journey.Features.Authentication.Signup.Commands;
using vsa_journey.Features.Authentication.RefreshToken.Command;
using vsa_journey.Features.Authentication.ForgotPassword.Commads;
using vsa_journey.Features.Authentication.ResetPassword.Commands;
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
        var createUserResult = await _mediator.Send(command);
        
        if (createUserResult.IsSuccess)
        {
            return Created(createUserResult.Value.Location,
                _apiResponse.Created(message: createUserResult.Successes!.FirstOrDefault()?.Message));
        }
        
        var requestId = HttpContext.TraceIdentifier;
        var errors = createUserResult.Errors.Select(error => error.Message);
        var requestPath = HttpContext.Request.Path;

        return BadRequest(_apiResponse.BadRequest(requestId, errors, requestPath));
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
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand command)
    {
        return await HandleMediatorResult(_mediator.Send(command));
    }

    [HttpPost]
    [Route("Reset-Password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        return await HandleMediatorResult(_mediator.Send(command));
    }

    private async Task<IActionResult> HandleMediatorResult<T>(Task<Result<T>> task)
    {
        var result = await task;
    
        if (result.IsSuccess)
        {
            var success = result.Successes.FirstOrDefault();
            var message =  success?.Message ?? "Operation successful";
            var data = result.ValueOrDefault;

            return Ok(_apiResponse.Ok(data, message));
        }

        var requestId = HttpContext.TraceIdentifier;
        var errors = result.Errors.Select(error => error.Message);
        var requestPath = HttpContext.Request.Path;

        return BadRequest(_apiResponse.BadRequest(requestId, errors, requestPath));
    }

    
}