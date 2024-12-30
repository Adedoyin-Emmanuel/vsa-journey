using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Events;

namespace vsa_journey.Features.Authentication.ForgotPassword.Commads;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<object>>
{
    private readonly UserManager<User> _userManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly IValidator<ForgotPasswordCommand> _validator;
    
    public async Task<Result<object>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}