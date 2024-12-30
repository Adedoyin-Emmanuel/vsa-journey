using MediatR;

namespace vsa_journey.Features.Authentication.ForgotPassword.Events;

public sealed class ForgotPasswordEvent : INotification
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ForgotPasswordCode { get; set; }


    public ForgotPasswordEvent(string firstName, string lastName, string email, string forgotPasswordCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ForgotPasswordCode = forgotPasswordCode;
    }
}