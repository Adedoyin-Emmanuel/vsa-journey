using System.Text;
using vsa_journey.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Domain.Constants;

namespace vsa_journey.Infrastructure.Services.Token;

public class TokenService : ITokenService
{

    private readonly UserManager<User> _userManager;


    public TokenService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    
    public async Task<string> GenerateAccessTokenAsync(User user)
    {
        var key = EnvConfig.JwtSecret;
        var issuer = EnvConfig.ValidIssuer;
        var audience = EnvConfig.ValidAudience;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
        };

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public async Task<string> GenerateAndStoreRefreshTokenAsync(User user)
    {
       await _userManager.RemoveAuthenticationTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken);
       var refreshToken = await _userManager.GenerateUserTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken);
       await _userManager.SetAuthenticationTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken, refreshToken);
      
       return refreshToken;
    }

    public async Task<string?> GetRefreshTokenAsync(User user)
    {
        return await _userManager.GetAuthenticationTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken);
    }

    public async Task<bool> VerifyRefreshTokenAsync(User user, string refreshToken)
    {
        return await _userManager.VerifyUserTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken, refreshToken);
    }

    public async Task<bool> RevokeRefreshTokenAsync(User user)
    {
        var result =
            await _userManager.RemoveAuthenticationTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken);
        
        return result.Succeeded;
    }
}