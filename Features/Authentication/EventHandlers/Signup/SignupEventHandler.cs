using MediatR;
using vsa_journey.Features.Authentication.Events.Signup;

namespace vsa_journey.Features.Authentication.EventHandlers.Signup;

public class SignupEventHandler : INotificationHandler<SignupEvent>
{
    public Task Handle(SignupEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Signup event received");
        Console.WriteLine($"Sending Verification Email to {notification.Email}");
        
        return Task.CompletedTask;
    }
}