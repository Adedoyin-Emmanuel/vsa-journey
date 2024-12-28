using MediatR;
using FluentResults;

namespace vsa_journey.Features.Authentication.RefreshToken.Command;

public class RefreshAccessTokenCommand : IRequest<Result<RefreshAccessTokenResponse>>
{
    public string RefreshToken { get; private set; }
}