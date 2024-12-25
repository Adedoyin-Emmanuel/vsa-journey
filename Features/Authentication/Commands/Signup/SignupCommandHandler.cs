using MediatR;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Features.Authentication.Commands.Signup;

public sealed class SignupCommandHandler : IRequestHandler<SignupCommand, Result>
{

    private readonly IValidator<SignupCommand> _validator;
    private readonly UserManager<User> _userManager;
    private readonly IEvent


    public SignupCommandHandler(IValidator<SignupCommand> validator, UserManager<User> userManager)
    {
        _validator = validator;
        _userManager = userManager;
    }
    public async Task Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        
        var user  = comman
        
    }
}