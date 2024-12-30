using FluentValidation;

namespace vsa_journey.Features.Authentication.ResetPassword.Commands;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress();
        RuleFor(command => command.PasswordResetCode).NotEmpty();
    }
}