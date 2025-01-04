using Microsoft.AspNetCore.Identity;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Persistence.Seeders;

namespace vsa_journey.Infrastructure.Extensions.ApplicationBuilder;

public class DatabaseSeeder
{
    
    private readonly RoleSeeder _roleSeeder;
    private readonly AppDbContext _dbContext;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(RoleSeeder roleSeeder, AppDbContext dbContext, ILogger<DatabaseSeeder> logger)
    {
        _roleSeeder = roleSeeder;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task UseSeedingAsync()
    {
        _dbContext.Database.EnsureCreatedAsync();
        
        try
        {
            await SeedRolesAsync();
            

        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while seeding {e}");
        }
    }
    
    private async Task SeedRolesAsync()
    {
        if (!_dbContext.Roles.Any())
        {
            _logger.LogInformation("Starting Role Seeding");
            await _roleSeeder.SeedRolesAsync();
            _logger.LogInformation("Role Seeding Completed Successfully");
        }
    }
    
    
}