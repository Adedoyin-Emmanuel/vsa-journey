using MediatR;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Features.Authentication.VerifyAccount.Commands;

public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, Result<object>>
{
    private readonly IValidator<VerifyAccountCommand> _validator;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<VerifyAccountCommandHandler> _logger;


    public VerifyAccountCommandHandler(IValidator<VerifyAccountCommand> validator, UserManager<User> userManager, ILogger<VerifyAccountCommandHandler> logger)
    {
        _validator = validator;
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task<Result<object>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return Result.Fail("Invalid payload");
        }
        
        _logger.LogInformation(request.VerificationCode);
        
        var isVerified = await _userManager.ConfirmEmailAsync(user, request.VerificationCode);

        return !isVerified.Succeeded ? Result.Fail("Invalid payload") : Result.Ok().WithSuccess("Account verified successfully");
    }
}