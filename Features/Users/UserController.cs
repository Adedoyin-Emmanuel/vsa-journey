using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using vsa_journey.Application.Responses;

namespace vsa_journey.Features.Users;

public interface IUser
{
    Guid Id { get; set; }
    string Name { get; set; }
    int Age { get; set; }
}


public class User : IUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int Age { get; set; }    
}


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
    public  IActionResult Get()
    {
        _logger.LogInformation("Getting users");
        IEnumerable<User> users = new List<User>
        {

            new User { Id = Guid.NewGuid(), Name = "Temmy girl", Age = 26 },
            new User { Id = Guid.NewGuid(), Name = "Femi Femo", Age = 24 },
            new User { Id = Guid.NewGuid(), Name = "Adedoyin Emmanuel", Age = 28 },

        };
        
        return Ok(_response.Success(users, "Users retrieved successfully"));
    }
    
}