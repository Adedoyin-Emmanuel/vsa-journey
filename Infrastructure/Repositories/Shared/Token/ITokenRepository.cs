using vsa_journey.Domain.Entities.Token;

namespace vsa_journey.Infrastructure.Repositories.Shared.Token;

public interface ITokenRepository : IRepository<Domain.Entities.Token.Token>
{
   public Task<Domain.Entities.Token.Token?> GetTokenByValueAsync(string tokenId);
   
   public Task<Domain.Entities.Token.Token?> GetTokenByUserIdAsync(Guid userId, TokenType tokenType);
}