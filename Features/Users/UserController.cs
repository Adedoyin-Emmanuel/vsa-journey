using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

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


    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public  IActionResult Get()
    {
        _logger.LogInformation("Getting users");
        IEnumerable<User> users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Name = "John Doe", Age = 32 },
            new User { Id = Guid.NewGuid(), Name = "Adedoyin Emmanuel", Age = 19 },

        };
        
        return Ok(users);
    }
    
}