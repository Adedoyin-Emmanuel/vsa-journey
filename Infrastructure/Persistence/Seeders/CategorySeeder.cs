using vsa_journey.Domain.Entities.Category;

namespace vsa_journey.Infrastructure.Persistence.Seeders;

public  class CategorySeeder
{

    public CategorySeeder()
    {
        
    }
    public async Task SeedCategoriesAsync(AppDbContext context)
    {
        var categories  = new List<Category>
        {
            new Category {Id  = new Guid(), Name = "Supplements and Vitamins" },
            new Category {Id  = new Guid(), Name = "Medical Devices and Supplies" },
            new Category {Id  = new Guid(), Name = "Personal Care and Hygiene" },
            new Category{Id = new Guid(), Name = "First Aid and Emergency Care"},
            
        }
        
    }
}