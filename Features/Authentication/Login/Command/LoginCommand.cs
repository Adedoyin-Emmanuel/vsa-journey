using MediatR;
using FluentResults;

namespace vsa_journey.Features.Authentication.Login.Commands;

public sealed record LoginCommand : IRequest<Result<LoginResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
   
}