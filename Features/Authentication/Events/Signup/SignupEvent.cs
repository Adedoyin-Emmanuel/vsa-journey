using MediatR;

namespace vsa_journey.Features.Authentication.Events.Signup;

public class SignupEvent : INotification
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public string VerificationCode { get; set; } 

    public SignupEvent(string firstName, string lastName, string email, string verificationCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VerificationCode = verificationCode;
    }
}