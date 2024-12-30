using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Features.Authentication.ResetPassword.Commands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<object>>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;
    private readonly IValidator<ResetPasswordCommand> _validator;

    public ResetPasswordCommandHandler(UserManager<User> userManager, ILogger<ResetPasswordCommandHandler> logger, IValidator<ResetPasswordCommand> validator)
    {
        _userManager = userManager;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<object>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null) return Result.Fail("Invalid payload");
        
        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, request.PasswordResetCode, )


    }
}