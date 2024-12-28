using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.Category;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Application.Common.PaginatedResult;

namespace vsa_journey.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class, IBase
{
    private readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context; 
        _dbSet = _context.Set<T>();
    }
    
    public async Task<T> AddAsync(T entity)
    {
        var createdEntity = await _dbSet.AddAsync(entity);
        return createdEntity.Entity;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<PaginatedResult<T>> GetAllAsync(int skip, int take)
    {
        var entityQuery = _dbSet.AsQueryable();

        int totalEntities = await entityQuery.CountAsync();

        var filteredEntityQuery = entityQuery.OrderBy(e => e.CreatedAt).Skip(skip).Take(take);

        var allEntities = await filteredEntityQuery.ToListAsync();

        return new PaginatedResult<T>
        {
            Skip = skip,
            Take = take,
            Total = totalEntities,
            Items = allEntities
        };
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updatedEntity = _dbSet.Update(entity);
        return updatedEntity.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity == null) return false;

        _dbSet.Remove(existingEntity);

        return true;
    }
}