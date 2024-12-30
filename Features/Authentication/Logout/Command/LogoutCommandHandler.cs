using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.Token;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Infrastructure.Repositories.Shared.Token;
using vsa_journey.Infrastructure.Services.Jwt;
using vsa_journey.Utils;

namespace vsa_journey.Features.Authentication.Logout.Command;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<object>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    private readonly ITokenRepository _tokenRepository;
    private readonly IJwtService _jwtService;
    private readonly ILogger<LogoutCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenCache _tokenCache;


    public LogoutCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ITokenRepository tokenRepository, IJwtService jwtService, ILogger<LogoutCommandHandler> logger, IUnitOfWork unitOfWork, JwtTokenCache tokenCache)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _jwtService = jwtService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _tokenCache = tokenCache;
    }
    
    
    public async Task<Result<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
       var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
       var jti = _httpContextAccessor?.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Jti);
       
       if(String.IsNullOrEmpty(userId)) return Result.Fail("User is not authenticated");
      
       var token = await _tokenRepository.GetTokenByUserIdAsync(Guid.Parse(userId), TokenType.RefreshToken);
       
       if(token is null) return Result.Fail("Logout failed");

       var revokeTokenResult = await _jwtService.RevokeRefreshTokenAsync(token.Value);

       await _tokenCache.InvalidateToken(jti!);
       
       return !revokeTokenResult ? Result.Fail("Logout failed") : Result.Ok().WithSuccess("Logout successful");
    }
}