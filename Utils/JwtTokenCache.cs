using StackExchange.Redis;

namespace vsa_journey.Utils;

public class JwtTokenCache
{
    
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    private const string JwtPrefix = "jwt";

    public JwtTokenCache(string connectionString)
    {
       _redis = ConnectionMultiplexer.Connect(connectionString);
       _db = _redis.GetDatabase();
    }

    public async Task StoreTokenAsync(string jti, string userId, TimeSpan expiresIn)
    {
        var key = $"{JwtPrefix}{jti}";
        var hashFields = new HashEntry[]
        {
            new HashEntry("userId", userId),
            new HashEntry("createdAt", DateTime.UtcNow.ToString("O")),
            new HashEntry("expiresAt", DateTime.UtcNow.Add(expiresIn).ToString("O"))
        };

        await _db.HashSetAsync(key, hashFields);
        await _db.KeyExpireAsync(key, expiresIn);

        var userTokensKey = $"user:{userId}:tokens";
        await _db.SetAddAsync(userTokensKey, key);
        await _db.KeyExpireAsync(userTokensKey, expiresIn);
    }


    private async Task<List<string>> GetTokensByUserIdAsync(string userId)
    {
        var userTokensKey = $"user:{userId}:tokens";
        var tokens = await _db.SetMembersAsync(userTokensKey);
        return tokens.Select(x => x.ToString()).ToList();
    }

    
    public async Task RevokeAllTokensByUserIdAsync(string userId)
    {
        var userTokensKey = $"user:{userId}:tokens";
        var tokens = await _db.SetMembersAsync(userTokensKey);

        foreach (var tokenKey in tokens)
        {
            await _db.KeyDeleteAsync(new RedisKey(tokenKey));  
        }

        await _db.KeyDeleteAsync(userTokensKey);
    }
    

    public async Task<bool> IsValidToken(string jti, string userId)
    {
        var userTokensKey = $"user:{userId}:tokens";
        
        return await _db.SetContainsAsync(userTokensKey, $"{JwtPrefix}{jti}");
    }

    
    public async Task<Dictionary<string, string>> GetTokenInfo(string jti)
    {
        var key = $"{JwtPrefix}{jti}";
        var hashFields = await _db.HashGetAllAsync(key);
        
        return hashFields.ToDictionary(
            x => x.Name.ToString(),
            x => x.Value.ToString()
        );
    }

    public async Task InvalidateToken(string jti)
    {
        var key = $"{JwtPrefix}{jti}";
        await _db.KeyDeleteAsync(key);
    }

   
    public async Task CleanExpiredTokens()
    {
        var server = _redis.GetServer(_redis.GetEndPoints().First());
        var pattern = $"{JwtPrefix}*";
        
        await foreach (var key in server.KeysAsync(pattern: pattern))
        {
            var ttl = await _db.KeyTimeToLiveAsync(key);
            if (!ttl.HasValue || ttl.Value.TotalMilliseconds <= 0)
            {
                await _db.KeyDeleteAsync(key);
            }
        }
    }

}