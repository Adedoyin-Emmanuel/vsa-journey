using vsa_journey.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace vsa_journey.Infrastructure.Persistence.Seeders;

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleSeeder(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task SeedRolesAsync()
    {
        var roles = new List<string>
        {
            Roles.SuperAdmin,
            Roles.Admin,
            Roles.SalesRepresentative,
            Roles.User
        };

        foreach (var role in roles)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role);

            if (roleExist) continue;
            
            var newRole = new IdentityRole<Guid>(role);
            await _roleManager.CreateAsync(newRole);
        }
    }
}