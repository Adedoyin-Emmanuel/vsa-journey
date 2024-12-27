using FluentEmail.Core;
using MediatR;
using vsa_journey.Features.Authentication.Events.Signup;

namespace vsa_journey.Features.Authentication.EventHandlers.Signup;

public class SignupEventHandler : INotificationHandler<SignupEvent>
{

    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger<SignupEventHandler> _logger;

    public SignupEventHandler(IFluentEmail fluentEmail, ILogger<SignupEventHandler> logger)
    {
        _fluentEmail = fluentEmail;
        _logger = logger;

    }
    
    
    public async Task Handle(SignupEvent notification, CancellationToken cancellationToken)
    {   
        
        _logger.LogInformation($"Sending verification email to {notification.Email}");
        var message =
            $"Hi {notification.FirstName} {notification.LastName}. Thank you for signing up. Below is your verification code {notification.VerificationCode}";
        
        await _fluentEmail
            .To(notification.Email)
            .Subject("Email Verification")
            .Body(message)
            .SendAsync();
    }
}