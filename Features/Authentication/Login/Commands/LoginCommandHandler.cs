using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Features.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<object>>
{
    
    private readonly UserManager<User> _userManager;
    //private readonly 


    public LoginCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<object>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
       await _userManager.verify
    }
}