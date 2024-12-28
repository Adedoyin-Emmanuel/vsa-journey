using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.Token;
using vsa_journey.Infrastructure.Persistence;

namespace vsa_journey.Infrastructure.Repositories.Shared.Token;

public class TokenRepository : Repository<Domain.Entities.Token.Token>, ITokenRepository
{
    public TokenRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.Entities.Token.Token?> GetTokenByValueAsync(string tokenValue)
    {
        var validToken = await _dbSet.FirstOrDefaultAsync(token => token.Value == tokenValue && token.IsRevoked == false && token.ExpiresAt > DateTime.UtcNow);
        
        return validToken;
    }

    public async Task<Domain.Entities.Token.Token?> GetTokenByUserIdAsync(Guid userId, TokenType tokenType)
    {
        var validToken = await _dbSet.FirstOrDefaultAsync(token => token.UserId == userId && token.Type == tokenType && token.IsRevoked == false && token.ExpiresAt > DateTime.UtcNow); 
        
        return validToken;
    }
    
    
}