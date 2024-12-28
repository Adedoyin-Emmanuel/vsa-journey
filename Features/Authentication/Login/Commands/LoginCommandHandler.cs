using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Services.Token;
using vsa_journey.Features.Authentication.Commands.Login;

namespace vsa_journey.Features.Authentication.Login.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<object>>
{
    
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;


    public LoginCommandHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<Result<object>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            Result.Fail("Invalid credentials");
        }

        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user!);

        if (!isEmailConfirmed)
        {
            Result.Fail("Email has not been confirmed");
        }

        var accessToken = await _tokenService.GenerateAccessTokenAsync(user!);
        var refreshToken = await _tokenService.GenerateAndStoreRefreshTokenAsync(user!);


        return Result.Ok(
            new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
    }
}