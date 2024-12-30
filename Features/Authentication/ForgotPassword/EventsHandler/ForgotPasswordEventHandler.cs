using FluentEmail.Core;
using MediatR;
using vsa_journey.Features.Authentication.ForgotPassword.Events;

namespace vsa_journey.Features.Authentication.ForgotPassword.EventsHandler;

public class ForgotPasswordEventHandler : INotificationHandler<ForgotPasswordEvent>
{
    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger<ForgotPasswordEventHandler> _logger;

    public ForgotPasswordEventHandler(IFluentEmail fluentEmail, ILogger<ForgotPasswordEventHandler> logger)
    {
        _fluentEmail = fluentEmail;
        _logger = logger;
    }
    
    public async Task Handle(ForgotPasswordEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sending forgot password email");
        
        var message = $@"
        Hi {notification.FirstName} {notification.LastName},

        We received a request to reset your password. If you made this request, please use the code below to reset your password:

        Reset Code: {notification.ForgotPasswordCode}

        If you did not request this change, please ignore this email or contact our support team for assistance.

        Thank you,  
        FolyCare Support Team
        ";
        
        await _fluentEmail
            .To(notification.Email)
            .Subject("Forgot Password Reset Code")
            .Body(message)
            .SendAsync(cancellationToken);
    }
}