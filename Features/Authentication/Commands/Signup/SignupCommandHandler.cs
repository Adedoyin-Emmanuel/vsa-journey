using FluentValidation;
using MediatR;

namespace vsa_journey.Features.Authentication.Commands.Signup;

public class SignupCommandHandler : IRequestHandler<SignupCommand>
{

    private readonly IValidator<SignupCommand> _validator;
    
    public async Task Handle(SignupCommand command, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(command);
        
        
    }
}