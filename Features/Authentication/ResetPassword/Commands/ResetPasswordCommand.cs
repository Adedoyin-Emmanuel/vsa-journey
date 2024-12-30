using MediatR;
using FluentResults;

namespace vsa_journey.Features.Authentication.ResetPassword.Commands;

public sealed record ResetPasswordCommand : IRequest<Result<object>>
{
    public string Email { get; init; }
    
    public string PasswordResetCode { get; set; }
    
    public string NewPassword { get; set; }
}
