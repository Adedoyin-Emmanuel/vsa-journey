using Microsoft.AspNetCore.Identity;

namespace vsa_journey.Domain.Entities.User;

public class User : IdentityUser<Guid>
{
    string FirstName { get; set; }
    
    DateTime CreatedAt { get; set; }
    
    DateTime UpdatedAt { get; set; }
}