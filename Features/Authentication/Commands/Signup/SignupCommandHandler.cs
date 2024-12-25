using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Utils;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
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
            return Result.Fail($"Email {command.Email} already exists.");
        }

        var newUser = _mapper.Map<User>(command);


        await _eventPublisher.PublishAsync();

        return Result.Ok().WithSuccess("User created. Please check your mail for verification code");
    }
}