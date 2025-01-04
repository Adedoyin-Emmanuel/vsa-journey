using vsa_journey.Domain.Entities.Product;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Products.Repository;

public class ProductRepository : Repository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
    
    
}