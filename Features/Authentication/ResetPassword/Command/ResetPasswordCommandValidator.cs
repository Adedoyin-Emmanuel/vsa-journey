using FluentValidation;

namespace vsa_journey.Features.Authentication.ResetPassword.Commands;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress();
        
        RuleFor(command => command.PasswordResetCode).NotEmpty();
        
        RuleFor(command => command.NewPassword).NotEmpty().MinimumLength(8).MaximumLength(30)
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");
    }
}