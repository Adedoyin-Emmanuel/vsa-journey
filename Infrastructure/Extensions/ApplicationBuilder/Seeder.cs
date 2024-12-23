using Microsoft.AspNetCore.Identity;
using vsa_journey.Infrastructure.Persistence.Seeders;

namespace vsa_journey.Infrastructure.Extensions.ApplicationBuilder;

public static class Seeder
{
    public static async Task UseSeedingAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger>();

        try
        {
            logger.LogInformation("Starting Role Seeding");
            await RoleSeeder.SeedRolesAsync(roleManager);
            logger.LogInformation("Role Seeding Completed Successfully");
        }
        catch (Exception e)
        {
            logger.LogError($"An error occurred while seeding {nameof(RoleSeeder)} - {e}");
        }
    }
}