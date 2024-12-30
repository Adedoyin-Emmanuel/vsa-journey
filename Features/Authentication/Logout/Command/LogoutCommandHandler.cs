using MediatR;
using FluentResults;
using vsa_journey.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Domain.Entities.Token;
using vsa_journey.Infrastructure.Services.Jwt;
using vsa_journey.Infrastructure.Repositories.Shared.Token;

namespace vsa_journey.Features.Authentication.Logout.Command;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<object>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtService _jwtService;
    private readonly JwtTokenCache _tokenCache;


    public LogoutCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ITokenRepository tokenRepository, IJwtService jwtService, JwtTokenCache tokenCache)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenRepository = tokenRepository;
        _jwtService = jwtService;
        _tokenCache = tokenCache;
    }
    
    
    public async Task<Result<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
       var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
       
       if(string.IsNullOrEmpty(userId)) return Result.Fail("User is not authenticated");
      
       var token = await _tokenRepository.GetTokenByUserIdAsync(Guid.Parse(userId), TokenType.RefreshToken);
       
       if(token is null) return Result.Fail("Logout failed");

       var revokeTokenResult = await _jwtService.RevokeRefreshTokenAsync(token.Value);

       await _tokenCache.RevokeAllTokensByUserIdAsync(userId);
       
       return !revokeTokenResult ? Result.Fail("Logout failed") : Result.Ok().WithSuccess("Logout successful");
    }
}