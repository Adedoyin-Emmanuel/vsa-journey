using MediatR;
using FluentResults;
using vsa_journey.Domain.Constants;

namespace vsa_journey.Features.Authentication.Signup.Commands;

public sealed record SignupCommand :  IRequest<Result>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AuthRole Role { get; set; } = AuthRole.User;
}