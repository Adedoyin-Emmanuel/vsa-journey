using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;
using vsa_journey.Application.Responses;
using vsa_journey.Domain.Constants;
using vsa_journey.Domain.Entities.Category;

namespace vsa_journey.Features.Users;


[ApiVersion(1)]
[ApiController]
[Route("v{v:apiVersion}/[controller]")]
public class UserController: ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IApiResponse _response;


    public UserController(ILogger<UserController> logger, IApiResponse response)
    {
        _logger = logger;
        _response = response;
    }

    
    [HttpGet]
    [Authorize(Roles=Roles.Admin)]
    public IActionResult Get()
    {
        var data = new List<string>
        {
            "temi",
            "emma"
        };
        return Ok(_response.Ok(data));
    }

    
}