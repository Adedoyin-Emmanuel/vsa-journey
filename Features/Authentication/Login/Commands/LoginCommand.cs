using MediatR;
using FluentResults;

namespace vsa_journey.Features.Authentication.Commands.Login;

public sealed record LoginCommand : IRequest<Result<object>>
{
    public string Email { get; set; }
    public string Password { get; set; }
   
}