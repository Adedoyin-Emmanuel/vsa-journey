using Microsoft.EntityFrameworkCore;
using vsa_journey.Infrastructure.Persistence;

namespace vsa_journey.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{

    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;


    public Repository(AppDbContext context, DbSet<T> dbSet)
    {
        _context = context; 
        _dbSet = dbSet; 
    }
    
    public async Task<T> AddAsync(T entity)
    {
       var createdEntity = await _dbSet.AddAsync(entity);
       
       return createdEntity.Entity;
    }

    public async Task<T?> GetByIdAsync(Guid Id)
    {
        var entity = await _dbSet.FindAsync(Id);

        return entity;
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