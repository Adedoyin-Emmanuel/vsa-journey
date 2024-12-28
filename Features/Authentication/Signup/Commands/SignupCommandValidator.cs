using FluentValidation;

namespace vsa_journey.Features.Authentication.Signup.Commands;

public class SignupCommandValidator : AbstractValidator<SignupCommand>
{
    public SignupCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress().MaximumLength(30);
        
        RuleFor(command => command.Password).NotEmpty().MinimumLength(8).MaximumLength(30)
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

        RuleFor(command => command.FirstName).NotEmpty().MaximumLength(20);

        RuleFor(command => command.LastName).NotEmpty().MaximumLength(20);

        RuleFor(command => command.Role).NotEmpty().IsInEnum();
        
    }
}