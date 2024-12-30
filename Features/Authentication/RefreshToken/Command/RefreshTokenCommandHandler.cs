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
    private readonly JwtTokenCache _tokenCache;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;


    public RefreshTokenCommandHandler(IJwtService jwtService, UserManager<User> userManager, ITokenRepository tokenRepository, ILogger<RefreshTokenCommandHandler> logger, JwtTokenCache tokenCache)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _tokenCache = tokenCache;
        _logger = logger;
    }

    public async Task<Result<RefreshAccessTokenResponse>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        
        var refreshToken = request.RefreshToken;
        
        var validToken = await _tokenRepository.GetTokenByValueAsync(refreshToken);
        
        if (validToken is null) return Result.Fail("Invalid request. Please try again.");
        
        var user = await _userManager.FindByIdAsync(validToken!.UserId.ToString());

        if (user == null)
        {
            _logger.LogWarning("User was not found");
            return Result.Fail("Invalid request. Please try again");
        }
        
        var isTokenValid = await _jwtService.VerifyRefreshTokenAsync(user, refreshToken);

        if (!isTokenValid)
        {
            return Result.Fail("Unauthorized. Please login");
        }
        

        await _jwtService.RevokeRefreshTokenAsync(refreshToken);
        await _tokenCache.RevokeAllTokensByUserIdAsync(user.Id.ToString());
        
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