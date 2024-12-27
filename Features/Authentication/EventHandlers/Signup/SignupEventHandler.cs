using FluentEmail.Core;
using MediatR;
using vsa_journey.Features.Authentication.Events.Signup;

namespace vsa_journey.Features.Authentication.EventHandlers.Signup;

public class SignupEventHandler : INotificationHandler<SignupEvent>
{

    private readonly IFluentEmail fluentEmail;

    public SignupEventHandler(IFluentEmail fluentEmail)
    {
        this.fluentEmail = fluentEmail;
    }
    public Task Handle(SignupEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine(notification);
        Console.WriteLine("Signup event received");
        Console.WriteLine($"Sending Verification Email to {notification.Email}");
        
        return Task.CompletedTask;
    }
}