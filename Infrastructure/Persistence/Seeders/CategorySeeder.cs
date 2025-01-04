using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.Category;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Infrastructure.Persistence.Seeders;

public  class CategorySeeder
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategorySeeder> _logger;

    public CategorySeeder(AppDbContext context, IUnitOfWork unitOfWork, ILogger<CategorySeeder> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task SeedCategoriesAsync()
    {
        var allCategories = new List<string>
        {

            "Prescription Drugs",
            "Over-the-Counter (OTC) Medications",
            "Supplements and Vitamins",
            "Herbal and Alternative Medicines",
            "Medical Devices and Supplies",
            "Diagnostics and Test Kits",
            "Personal Care and Hygiene",
            "Oral Care (Toothpaste, Mouthwash, etc.)",
            "Hair Care (Shampoos, Conditioners, Treatments)",
            "Skincare (Lotions, Creams, Sunscreens)",
            "Cosmetics and Beauty",
            "Men's Grooming and Essentials",
            "Women's Health and Maternity",
            "Baby Care and Essentials",
            "Child Health and Nutrition",
            "Elderly Care and Mobility Aids",
            "First Aid and Emergency Care",
            "Pain Relief and Muscle Care",
            "Respiratory Care (Inhalers, Vaporizers, etc.)",
            "Eye Care (Drops, Contact Lenses, Solutions)",
            "Hearing Care and Accessories",
            "Pet Care and Veterinary Products",
            "Groceries and Packaged Foods",
            "Fresh Produce and Dairy Alternatives",
            "Beverages (Soft Drinks, Juices, Water)",
            "Snacks and Confectioneries",
            "Frozen Foods and Ready-to-Eat Meals",
            "Household Cleaning and Essentials",
            "Laundry and Detergents",
            "Fitness and Wellness Products",
            "Sports Nutrition and Energy Supplements",
            "Seasonal and Holiday Items",
            "Gift and Specialty Products",
            "Books and Magazines",
            "Stationery and Office Supplies",
            "Travel and Convenience Items"
        };


        foreach (var categoryName in allCategories)
        {
            try
            {
                var existingCategory =
                    await _context.Categories.FirstOrDefaultAsync(category => category.Name == categoryName);

                if (existingCategory is not null) continue;
                
                var newCategory = new Category()
                {
                    Name = categoryName,
                    Id = Guid.NewGuid(),
                };
                    
                await _context.Categories.AddAsync(newCategory);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception($"Could not create category {categoryName}");
            }
        }
    }
}