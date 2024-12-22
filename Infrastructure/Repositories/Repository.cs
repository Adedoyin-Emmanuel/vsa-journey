namespace vsa_journey.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    public async Task<T> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAllAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }
}