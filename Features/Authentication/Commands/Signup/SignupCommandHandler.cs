using MediatR;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Events;

namespace vsa_journey.Features.Authentication.Commands.Signup;

public sealed class SignupCommandHandler : IRequestHandler<SignupCommand, Result>
{

    private readonly IValidator<SignupCommand> _validator;
    private readonly UserManager<User> _userManager;
    private readonly IEventPublisher _eventPublisher;


    public SignupCommandHandler(IValidator<SignupCommand> validator, UserManager<User> userManager)
    {
        _validator = validator;
        _userManager = userManager;
    }
    public async Task<Result> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);

        var existingUser = await _userManager.FindByEmailAsync(command.Email);

        if (existingUser is not null)
        {
            return Result.Fail($"Email {command.Email} already exists.");
        }

        var newUser = new User{FirstName = command.FirstName, }
    }
}