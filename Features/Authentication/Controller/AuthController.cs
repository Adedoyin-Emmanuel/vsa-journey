using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Application.Responses;
using vsa_journey.Features.Authentication.Commands.Signup;

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
        
        if (!result.IsFailed) return Ok(_apiResponse.Success(message:result.Successes[0].Message));
        
        var requestId = HttpContext.TraceIdentifier;
        var errors = result.Errors;
        var requestPath = HttpContext.Request.Path;
            
        return BadRequest(_apiResponse.BadRequest(requestId, errors, requestPath));

    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }

    [HttpPost]
    [Route("Verify")]
    public async Task<IActionResult> Verify()
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