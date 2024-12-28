using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.Category;

namespace vsa_journey.Domain.Entities.User;

public class User : IdentityUser<Guid>, IBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}