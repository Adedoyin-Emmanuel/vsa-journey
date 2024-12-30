using FluentResults;
using MediatR;

namespace vsa_journey.Features.Authentication.ForgotPassword.Commads;

public sealed record ForgotPasswordCommand : IRequest<Result<object>>
{
    public string Email { get; set; }
}