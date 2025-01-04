using FluentResults;
using MediatR;

namespace vsa_journey.Features.Authentication.VerifyAccount.Commands;

public sealed record VerifyAccountCommand : IRequest<Result<object>>
{
    public string Email {get;  set;}
    public string VerificationCode {get;  set;}
}