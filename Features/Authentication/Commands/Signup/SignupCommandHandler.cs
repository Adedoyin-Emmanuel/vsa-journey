using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using vsa_journey.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vsa_journey.Domain.Constants;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Features.Authentication.Events.Signup;
using vsa_journey.Infrastructure.Events;

namespace vsa_journey.Features.Authentication.Commands.Signup;

public sealed class SignupCommandHandler : IRequestHandler<SignupCommand, Result>
{

    private readonly IValidator<SignupCommand> _validator;
    private readonly UserManager<User> _userManager;
    private readonly IEventPublisher _eventPublisher;
    private readonly UsernameGenerator _usernameGenerator;
    private readonly IMapper _mapper;
    private readonly ILogger<SignupCommandHandler> _logger;


    public SignupCommandHandler(IValidator<SignupCommand> validator, UserManager<User> userManager, IEventPublisher eventPublisher, UsernameGenerator usernameGenerator, IMapper mapper, ILogger<SignupCommandHandler> logger)
    {
        _validator = validator;
        _userManager = userManager;
        _eventPublisher = eventPublisher;
        _usernameGenerator = usernameGenerator;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Result> Handle(SignupCommand command, CancellationToken cancellationToken)
    {
         _validator.ValidateAndThrow(command);

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
        
        var verificationCode = "123456";
        
        var eventBody = new SignupEvent(newUser.FirstName, newUser.LastName, newUser.Email, verificationCode);
        
        await _eventPublisher.PublishAsync(eventBody);
        
        return Result.Ok().WithSuccess("Account created successfully. Please check your email.");
    }
}