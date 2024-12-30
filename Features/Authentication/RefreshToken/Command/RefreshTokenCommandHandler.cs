using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Repositories.Shared.Token;
using vsa_journey.Infrastructure.Services.Jwt;
using vsa_journey.Utils;

namespace vsa_journey.Features.Authentication.RefreshToken.Command;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, Result<RefreshAccessTokenResponse>>
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<User> _userManager;
    private readonly ITokenRepository _tokenRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtTokenCache _tokenCache;


    public RefreshTokenCommandHandler(IJwtService jwtService, UserManager<User> userManager, ITokenRepository tokenRepository, IHttpContextAccessor httpContextAccessor, JwtTokenCache tokenCache)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _httpContextAccessor = httpContextAccessor;
        _tokenCache = tokenCache;
    }

    public async Task<Result<RefreshAccessTokenResponse>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var validToken = await _tokenRepository.GetTokenByValueAsync(request.RefreshToken);
        var jti = _httpContextAccessor.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Jti);
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        
        if (validToken is null) return Result.Fail("Invalid request. Please try again.");
        
        var user = await _userManager.FindByIdAsync(validToken!.UserId.ToString());

        if (user == null)
        {
            return Result.Fail("Invalid request. Please try again");
        }
        
        var refreshToken = request.RefreshToken;
        
        var isTokenValid = await _jwtService.VerifyRefreshTokenAsync(user, refreshToken);

        if (!isTokenValid)
        {
            return Result.Fail("Unauthorized. Please login");
        }

        await _jwtService.RevokeRefreshTokenAsync(refreshToken);
        await _tokenCache.InvalidateToken(jti!);
        
        var newAccessToken = await _jwtService.GenerateAccessTokenAsync(user);
        var newRefreshToken = await _jwtService.GenerateAndStoreRefreshTokenAsync(user);
        
        
        var response = new RefreshAccessTokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
        
        return Result.Ok(response).WithSuccess("Access token refreshed successfully");
        
    }
}