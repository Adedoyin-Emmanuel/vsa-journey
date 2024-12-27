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
        var result =  await _mediator.Send(command);
        
        if (result.IsSuccess)
        {
            var successMessage = result.Successes.First().Message;
            return Ok(_apiResponse.Success(message: successMessage));
        }
        
        var requestId = HttpContext.TraceIdentifier;

        var errors = result.Errors.Select(error => new
        {
            Name = error.Metadata["Name"],
            Message = error.Message
        });
        
        var requestPath = HttpContext.Request.Path;

        return BadRequest(_apiResponse.BadRequest(requestId, errors, requestPath));
    }
    
    [HttpPost]
    [Route("Verify")]
    public async Task<IActionResult> Verify(VerifyAccountCommand command)
    {
        
        var result =  await _mediator.Send(command);
        
        if (!result.IsFailed) return Ok(_apiResponse.Success(message:"adsgsdgasdg"));
        
        var requestId = HttpContext.TraceIdentifier;
        var requestPath = HttpContext.Request.Path;
        
        return BadRequest(_apiResponse.BadRequest(requestId, result.Errors, requestPath));
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
     
    
    
    
    
}