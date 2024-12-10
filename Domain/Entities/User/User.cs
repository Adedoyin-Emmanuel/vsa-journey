using Microsoft.AspNetCore.Identity;

namespace vsa_journey.Domain.Entities.User;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}