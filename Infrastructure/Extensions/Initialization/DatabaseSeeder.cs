using Microsoft.AspNetCore.Identity;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Persistence.Seeders;

namespace vsa_journey.Infrastructure.Extensions.ApplicationBuilder;

public static class DatabaseSeeder
{
    public static async Task UseSeedingAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Seeder");
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.EnsureCreatedAsync();
        
        

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