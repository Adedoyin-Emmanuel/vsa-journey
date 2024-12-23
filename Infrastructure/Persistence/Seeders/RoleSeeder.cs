using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using vsa_journey.Domain.Constants;

namespace vsa_journey.Infrastructure.Persistence.Seeders;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
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
            var roleExist = await roleManager.RoleExistsAsync(role);

            if (!roleExist)
            {
                var newRole = new IdentityRole<Guid>(role);
                await roleManager.CreateAsync(newRole);
            }
        }
    }
}