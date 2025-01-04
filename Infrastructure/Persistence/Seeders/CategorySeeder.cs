namespace vsa_journey.Infrastructure.Persistence.Seeders;

public static class CategorySeeder
{
    private readonly ILogger<CategorySeeder> _logger;
    public static async Task SeedCategoriesAsync(AppDbContext context)
    {
        if (!context.Categories.Any())
        {
            
        }
    }
}