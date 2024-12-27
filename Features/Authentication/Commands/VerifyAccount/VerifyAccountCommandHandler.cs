using MediatR;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Features.Authentication.Commands.Signup;

namespace vsa_journey.Features.Authentication.Commands.VerifyAccount;

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
    
    public async Task<Result> Handle(VerifyAccountCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);
        
        return Result.Ok();
    }
}