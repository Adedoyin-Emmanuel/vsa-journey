using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Users.Repository;

public class UserRepository : Repository<User>, IUserRespository
{
    public UserRepository(AppDbContext context, DbSet<User> dbSet) : base(context, dbSet)
    {
        
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}