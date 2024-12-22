using Microsoft.EntityFrameworkCore;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Extensions.PaginatedResult;

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

    public async Task<PaginatedResult<T>> GetAllAsync(int skip, int take)
    {
        var entityQuery = _dbSet.AsQueryable();

        int totalEntities = await entityQuery.CountAsync();

        var filteredEntityQuery = entityQuery.OrderBy(e => e.CreatedAt).Skip(skip).Take(take);
        
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