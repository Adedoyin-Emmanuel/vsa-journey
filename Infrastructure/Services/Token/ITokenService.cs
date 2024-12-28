using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Infrastructure.Services.Token;

public interface ITokenService 
{
    public Task<string> GenerateAccessTokenAsync(User user);
    
    public Task<string> GenerateAndStoreRefreshTokenAsync(User user);
    
    public Task<string> GetRefreshTokenAsync(User user);
    
    public Task<bool> VerifyRefreshTokenAsync(User user, string refreshToken);
    
    public Task<bool> RevokeRefreshTokenAsync(User user);
}