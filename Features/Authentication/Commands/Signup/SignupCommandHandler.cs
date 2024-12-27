using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Utils;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Events;
using vsa_journey.Features.Authentication.Events.Signup;

namespace vsa_journey.Features.Authentication.Commands.Signup;

public sealed class SignupCommandHandler : IRequestHandler<SignupCommand, Result>
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
    public async Task<Result> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
         await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var existingUser = await _userManager.FindByEmailAsync(command.Email);

        if (existingUser is not null)
        {
            var errors = new List<IError>
            {
                new Error("Email already exists.")
                    .WithMetadata("Name", "Email") 
            };

            return Result.Fail(errors);
        }

        var newUser = _mapper.Map<User>(command);

        newUser.UserName = _usernameGenerator.GenerateUsername(newUser.FirstName, newUser.LastName);
        
        const string verificationCode = "123456";
        
        var eventBody = new SignupEvent(newUser.FirstName, newUser.LastName, newUser.Email, verificationCode);
        
        await _eventPublisher.PublishAsync(eventBody);
        
        return Result.Ok().WithSuccess("Account created successfully. Please check your email.");
    }
}