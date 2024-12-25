using MediatR;

namespace vsa_journey.Features.Authentication.Commands.Signup;

public sealed record SignupCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}