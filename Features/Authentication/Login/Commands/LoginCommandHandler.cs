using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.Token;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Repositories.Shared.Token;
using vsa_journey.Infrastructure.Services.Token;

namespace vsa_journey.Features.Authentication.Login.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ITokenRepository _tokenRepository;


    public LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService, ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _tokenRepository = tokenRepository;
    }
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
           return Result.Fail("Invalid credentials");
        }
        
        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user!);

        if (!isEmailConfirmed)
        {
            return Result.Fail("Email has not been confirmed");
        }

        var validRefreshToken = await _tokenRepository.GetTokenByUserIdAsync(user.Id, TokenType.RefreshToken);
      
        await _tokenService.RevokeRefreshTokenAsync(validRefreshToken?.Value!);
        
        var accessToken = await _tokenService.GenerateAccessTokenAsync(user!);
        var refreshToken = await _tokenService.GenerateAndStoreRefreshTokenAsync(user!);

        var response = new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };

        return Result.Ok(response).WithSuccess("Login successful");
    }
}