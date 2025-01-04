using MediatR;
using AutoMapper;
using FluentResults;
using FluentValidation;
using vsa_journey.Utils;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Events;
using vsa_journey.Features.Authentication.Signup.Events;

namespace vsa_journey.Features.Authentication.Signup.Commands;

public sealed class SignupCommandHandler : IRequestHandler<SignupCommand, Result<SignupResponse>>
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
    public async Task<Result<SignupResponse>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            return Result.Fail("Email already exists");
        }
        
        
        var newUser = _mapper.Map<User>(request);

        newUser.UserName = await _usernameGenerator.GenerateUsernameAsync(newUser.FirstName, newUser.LastName);
        
        var isCreated = await _userManager.CreateAsync(newUser, request.Password);

        if (!isCreated.Succeeded)
        {
            return Result.Fail("An error occured while signing up");
        }

        var role = request.Role.ToString();
        
        _logger.LogInformation($"User role is {role}");
        
        var assignRoleResult = await _userManager.AddToRoleAsync(newUser, role);

        if (!assignRoleResult.Succeeded)
        {
            _logger.LogInformation("An error occured while assigning role to user");
            return Result.Fail("An error occured while signing up");
        }

        var verificationCode = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
        
        var eventBody = new SignupEvent(newUser.FirstName, newUser.LastName, newUser!.Email, verificationCode);
        
        await _eventPublisher.PublishAsync(eventBody);

        var signupResponse = new SignupResponse
        {
            Location = $"/v1/user/{newUser.Id}"
        };
        
       return Result.Ok(signupResponse).WithSuccess("Account created. Check your email for verification code.");
    }
}