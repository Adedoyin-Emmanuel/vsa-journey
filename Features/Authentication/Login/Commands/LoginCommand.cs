using MediatR;

namespace vsa_journey.Features.Authentication.Commands.Login;

public sealed record LoginCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
   
}