using Microsoft.AspNetCore.Identity;
using vsa_journey.Infrastructure.Persistence.Seeders;

namespace vsa_journey.Infrastructure.Extensions.ApplicationBuilder;

public static class Seeder
{
    public static async Task UseSeedingAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await RoleSeeder.SeedRolesAsync(roleManager);
    }
}