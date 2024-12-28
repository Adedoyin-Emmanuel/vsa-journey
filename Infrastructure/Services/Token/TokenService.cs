using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using vsa_journey.Domain.Constants;
using vsa_journey.Domain.Entities.Token;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Infrastructure.Repositories.Shared.Token;
using vsa_journey.Utils;

namespace vsa_journey.Infrastructure.Services.Token;

public class TokenService : ITokenService
{

    private readonly UserManager<User> _userManager;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUnitOfWork _unitOfWork;


    public TokenService(UserManager<User> userManager, ITokenRepository tokenRepository, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _unitOfWork = unitOfWork;
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

       var customRefreshToken = new Domain.Entities.Token.Token
       {
            Value = refreshToken,
            UserId = user.Id,
            Type = TokenType.RefreshToken,
            ExpiresAt = DateTime.Now.AddDays(7)
       };
       await _tokenRepository.AddAsync(customRefreshToken);
       await _userManager.SetAuthenticationTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken, refreshToken);
       
       await _unitOfWork.SaveChangesAsync();
       
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

    public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
    {

        var validCustomToken = await _tokenRepository.GetTokenByValueAsync(refreshToken);

        if (validCustomToken == null) return false;

        var user = await _userManager.FindByIdAsync(validCustomToken.UserId.ToString());
        
        if (user == null) return false;

      
        var result =
            await _userManager.RemoveAuthenticationTokenAsync(user, nameof(TokenService), AuthToken.RefreshToken);

        if (!result.Succeeded) return false;
        
        validCustomToken.IsRevoked = true;
        await _unitOfWork.SaveChangesAsync();
            
        return true;

    }
}