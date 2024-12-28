using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Repositories;

namespace vsa_journey.Features.Users.Repository;

public interface IUserRespository : IRepository<User>
{
    public Task<bool> GetUserByUsernameAsync(string username);
}