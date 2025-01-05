using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Category.GetAllCategories.Repository;

public interface ICategoryRepository : IRepository<Domain.Entities.Category.Category>
{
    
}