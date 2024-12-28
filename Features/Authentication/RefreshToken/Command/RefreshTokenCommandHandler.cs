using System.Security.Claims;
using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Services.Token;

namespace vsa_journey.Features.Authentication.RefreshToken.Command;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, Result<RefreshAccessTokenResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public RefreshTokenCommandHandler(ITokenService tokenService, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<RefreshAccessTokenResponse>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);
        
        Console.WriteLine(userId);
        Console.WriteLine(user);

        if (user == null)
        {
            return Result.Fail("Invalid request. Please try again");
        }
        
        var refreshToken = request.RefreshToken;
        
        var isTokenValid = await _tokenService.VerifyRefreshTokenAsync(user, refreshToken);

        if (!isTokenValid)
        {
            return Result.Fail("Unauthorized. Please login");
        }

        var newAccessToken = await _tokenService.GenerateAccessTokenAsync(user);

        var response = new RefreshAccessTokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = refreshToken
        };
        
        return Result.Ok(response).WithSuccess("Access token refreshed successfully");
        
    }
}