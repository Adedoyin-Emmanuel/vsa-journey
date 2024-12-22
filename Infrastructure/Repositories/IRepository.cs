namespace vsa_journey.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    public Task<T> AddAsync(T entity);

    public Task<T?> GetByIdAsync(Guid Id);

    public Task<IEnumerable<T>> GetAllAsync(int skip, int take);

    public Task UpdateAsync(T entity);
    
    public Task DeleteAsync(T entity);

}