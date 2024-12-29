using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Repositories.Shared.Token;
using vsa_journey.Infrastructure.Services.Token;

namespace vsa_journey.Features.Authentication.RefreshToken.Command;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, Result<RefreshAccessTokenResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly ITokenRepository _tokenRepository;


    public RefreshTokenCommandHandler(ITokenService tokenService, UserManager<User> userManager, ITokenRepository tokenRepository)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }

    public async Task<Result<RefreshAccessTokenResponse>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var validToken = await _tokenRepository.GetTokenByValueAsync(request.RefreshToken);
        
        if (validToken is null) return Result.Fail("Invalid request. Please try again.");
        
        var user = await _userManager.FindByIdAsync(validToken!.UserId.ToString());

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