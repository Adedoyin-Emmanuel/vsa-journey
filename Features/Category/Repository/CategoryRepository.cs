using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Category.GetAllCategories.Repository;

public class CategoryRepository : Repository<Domain.Entities.Category.Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}