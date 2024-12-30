using MediatR;
using FluentResults;

namespace vsa_journey.Features.Authentication.RefreshToken.Command;

public sealed record RefreshAccessTokenCommand : IRequest<Result<RefreshAccessTokenResponse>>
{
    public string RefreshToken { get;  set; }
}