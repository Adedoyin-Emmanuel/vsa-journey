using MediatR;

namespace vsa_journey.Features.Authentication.Events.Signup;

public class UserSignupEvent : INotification
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public UserSignupEvent(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}