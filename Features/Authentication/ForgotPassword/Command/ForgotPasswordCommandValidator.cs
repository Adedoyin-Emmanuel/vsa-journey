using FluentValidation;

namespace vsa_journey.Features.Authentication.ForgotPassword.Commads;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress();
    }
}