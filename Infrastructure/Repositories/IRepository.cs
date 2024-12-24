using vsa_journey.Domain.Entities.Category;
using vsa_journey.Application.Common.PaginatedResult;

namespace vsa_journey.Infrastructure.Repositories;

public interface IRepository<T> where T : class, IBase
{
    public Task<T> AddAsync(T entity);

    public Task<T?> GetByIdAsync(Guid id);

    public Task<PaginatedResult<T>> GetAllAsync(int skip, int take);

    public Task<T> UpdateAsync(T entity);
    
    public Task<bool> DeleteAsync(Guid id);

}