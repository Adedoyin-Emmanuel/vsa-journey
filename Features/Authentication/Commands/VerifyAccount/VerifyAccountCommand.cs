using MediatR;
using FluentResults;

namespace vsa_journey.Features.Authentication.Commands.VerifyAccount;

public sealed record VerifyAccountCommand : IRequest<Result>
{
    public string Email {get;  set;}
    public string VerificationCode {get;  set;}
}