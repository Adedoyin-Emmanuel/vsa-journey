using vsa_journey.Domain.Entities.Product;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Products.Repository;

public interface IProductRepository : IRepository<Product>
{
    
}