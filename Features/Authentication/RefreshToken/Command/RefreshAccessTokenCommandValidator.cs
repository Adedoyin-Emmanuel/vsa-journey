using FluentValidation;

namespace vsa_journey.Features.Authentication.RefreshToken.Command;

public class RefreshAccessTokenCommandValidator : AbstractValidator<RefreshAccessTokenCommand>
{
    public RefreshAccessTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken).NotEmpty();
       
        RuleFor(command => command.AccessToken).NotEmpty();
    }
}