using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Utils;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Features.Authentication.Signup.Events;
using vsa_journey.Infrastructure.Events;

namespace vsa_journey.Features.Authentication.Signup.Commands;

public sealed class SignupCommandHandler : IRequestHandler<SignupCommand, Result<User>>
{

    private readonly IValidator<SignupCommand> _validator;
    private readonly UserManager<User> _userManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly IMapper _mapper;
    private readonly ILogger<SignupCommandHandler> _logger;
    private readonly UsernameGenerator _usernameGenerator;


    public SignupCommandHandler(IValidator<SignupCommand> validator, UserManager<User> userManager, IEventPublisher eventPublisher, IMapper mapper, ILogger<SignupCommandHandler> logger, UsernameGenerator usernameGenerator)
    {
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
        _userManager = userManager;
        _eventPublisher = eventPublisher;
        _usernameGenerator = usernameGenerator;
    }
    public async Task<Result<User>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
         await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            var errors = new List<IError>
            {
                new Error("Email already exists.")
            };

            return Result.Fail<User>(errors);
        }

        var newUser = _mapper.Map<User>(request);

        newUser.UserName = _usernameGenerator.GenerateUsername(newUser.FirstName, newUser.LastName);
        
        const string verificationCode = "123456";
        
        var eventBody = new SignupEvent(newUser.FirstName, newUser.LastName, newUser.Email, verificationCode);
        
        await _eventPublisher.PublishAsync(eventBody);

        return Result.Ok().WithSuccess("Account created. Check your email for verification code.");
    }
}