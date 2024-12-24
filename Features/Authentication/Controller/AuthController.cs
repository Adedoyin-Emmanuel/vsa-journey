using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace vsa_journey.Features.Authentication.Controller;

[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;


    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
     
    
    
    
    
}