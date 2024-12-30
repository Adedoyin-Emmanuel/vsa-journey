using MediatR;
using FluentResults;

namespace vsa_journey.Features.Authentication.Logout.Command;

public sealed record LogoutCommand : IRequest<Result<object>>
{
    
}