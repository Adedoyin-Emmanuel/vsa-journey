using System.Data;
using FluentValidation;

namespace vsa_journey.Features.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress();
        
        RuleFor(command => command.Password).NotEmpty().NotNull();
    }
}