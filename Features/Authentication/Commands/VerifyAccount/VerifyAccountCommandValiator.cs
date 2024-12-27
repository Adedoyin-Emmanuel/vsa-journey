using System.Data;
using FluentValidation;

namespace vsa_journey.Features.Authentication.Commands.VerifyAccount;

public class VerifyAccountCommandValiator : AbstractValidator<VerifyAccountCommand>
{
    public VerifyAccountCommandValiator()
    {
        RuleFor(command => command.Email).NotEmpty().EmailAddress();

        RuleFor(command => command.VerificationCode).NotEmpty().MaximumLength(10);
    }
}