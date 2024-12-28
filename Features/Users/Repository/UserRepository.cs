using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Users.Repository;

public class UserRepository : Repository<User>, IUserRespository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> GetUserByUsernameAsync(string username)
    {
        var result=  await _dbSet.FirstOrDefaultAsync(user => user.UserName == username);

        var flag = (result is not null) ? true :false;
        
        return flag;
    }
}