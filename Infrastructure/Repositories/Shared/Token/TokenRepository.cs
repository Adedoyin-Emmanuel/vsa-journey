using vsa_journey.Infrastructure.Persistence;

namespace vsa_journey.Infrastructure.Repositories.Shared.Token;

public class TokenRepository : Repository<Domain.Entities.Token.Token>, ITokenRepository
{
    public TokenRepository(AppDbContext context) : base(context)
    {
    }
}