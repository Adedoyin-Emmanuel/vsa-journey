using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Features.Authentication.ForgotPassword.Events;
using vsa_journey.Infrastructure.Events;

namespace vsa_journey.Features.Authentication.ForgotPassword.Commads;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<object>>
{
    private readonly UserManager<User> _userManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly IValidator<ForgotPasswordCommand> _validator;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;

    public ForgotPasswordCommandHandler(UserManager<User> userManager, IEventPublisher eventPublisher, IValidator<ForgotPasswordCommand> validator, ILogger<ForgotPasswordCommandHandler> logger)
    {
        _userManager = userManager;
        _eventPublisher = eventPublisher;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<object>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
       await _validator.ValidateAsync(request, cancellationToken);

       var user = await _userManager.FindByEmailAsync(request.Email);

       if (user is null || !user.EmailConfirmed) return Result.Fail("Invalid request");
       
       var token = await _userManager.GeneratePasswordResetTokenAsync(user);

       var eventBody = new ForgotPasswordEvent(user.FirstName, user.LastName, user.Email!, token);

       await _eventPublisher.PublishAsync(eventBody);
       
       return Result.Ok().WithSuccess("An OTP has been sent to your email");
    }
}