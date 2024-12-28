using MediatR;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;

namespace vsa_journey.Features.Authentication.VerifyAccount.Commands;

public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, Result>
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
    
    public async Task<Result> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            var errors = new List<IError>
            {
                new Error("Invalid payload").WithMetadata("Name", "Body")
            };
            
            return Result.Fail(errors);
        }

        return Result.Ok().WithSuccess("Account verified successfully");
    }
}