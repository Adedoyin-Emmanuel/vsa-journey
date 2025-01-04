using FluentValidation;

namespace vsa_journey.Features.Authentication.Login.Commands;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress();

        RuleFor(command => command.Password).NotEmpty();
    }
}