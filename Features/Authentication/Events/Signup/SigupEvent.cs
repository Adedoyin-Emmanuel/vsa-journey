using MediatR;

namespace vsa_journey.Features.Authentication.Events.Signup;

public class SigupEvent : INotification
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public SigupEvent(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}