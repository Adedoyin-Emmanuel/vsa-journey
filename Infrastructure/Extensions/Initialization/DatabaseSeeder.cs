using Microsoft.AspNetCore.Identity;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Persistence.Seeders;

namespace vsa_journey.Infrastructure.Extensions.ApplicationBuilder;

public class DatabaseSeeder
{
    
    private readonly RoleSeeder _roleSeeder;
    private readonly AppDbContext _dbContext;
    private readonly CategorySeeder _categorySeeder;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(RoleSeeder roleSeeder, CategorySeeder categorySeeder, AppDbContext dbContext, ILogger<DatabaseSeeder> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
        _roleSeeder = roleSeeder;
        _categorySeeder = categorySeeder;
    }

    public async Task UseSeedingAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
        
        try
        {
            await SeedRolesAsync();
            await SeedCategoriesAsync();

        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred while seeding {e}");
            throw new Exception("An error occurred while seeding database", e);
        }
    }
    
    private async Task SeedRolesAsync()
    {
        _logger.LogInformation("Starting Role Seeding");
        await _roleSeeder.SeedRolesAsync();
        _logger.LogInformation("Role Seeding Completed Successfully");
    }

    private async Task SeedCategoriesAsync()
    {
        _logger.LogInformation("Starting Category Seeding");
        await _categorySeeder.SeedCategoriesAsync();
        _logger.LogInformation("Category Seeding Completed Successfully");
    }
    
    
}