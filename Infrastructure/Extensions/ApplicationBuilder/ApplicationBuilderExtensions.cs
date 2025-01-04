using MediatR;
using Serilog;
using FluentValidation;
using vsa_journey.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Infrastructure.Events;
using vsa_journey.Application.Responses;
using vsa_journey.Application.Behaviours;
using vsa_journey.Features.Users.Repository;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Middlewares;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Infrastructure.Services.Jwt;
using vsa_journey.Features.Products.Repository;
using vsa_journey.Features.Authentication.Tokens;
using vsa_journey.Features.Authentication.Policies;
using vsa_journey.Features.Authentication.Extensions;
using vsa_journey.Infrastructure.Repositories.Shared.Token;


namespace vsa_journey.Infrastructure.Extensions.Services;

public static class ApplicationBuilderExtensions
{
    public static IServiceCollection  AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IApiResponse, ApiResponse>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRespository, UserRepository>();
        services.AddScoped<ITokenRepository,TokenRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddScoped<UsernameGenerator>();
        services.AddScoped<IJwtService, JwtService>();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddSingleton<JwtTokenCache>(provider => new JwtTokenCache(EnvConfig.RedisConnectionString));

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddJwtBearerAuthentication(services.BuildServiceProvider());
        services.AddAuthorization(options => options.AddCustomPolicies());
        services.AddCustomCookieAuthentication(services.BuildServiceProvider());
        
        return services;
    }


    public static IServiceCollection AddSwaggerAndApiVersioning(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCustomApiVersion();
        
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 36));
        services.AddDbContext<AppDbContext>(options => options.UseMySql(EnvConfig.DatabaseUrl, mySqlServerVersion));
        
        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddTokenProvider<CustomEmailConfirmationTokenProvider<User>>(nameof(CustomEmailConfirmationTokenProviderOptions))
            .AddTokenProvider<CustomForgotPasswordTokenProvider<User>>(nameof(CustomForgotPasswordTokenProviderOptions))
            .AddTokenProvider<DataProtectorTokenProvider<User>>(nameof(JwtService))
            .AddDefaultTokenProviders();
        
        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(7));

        return services;
    }

    public static IServiceCollection AddAutoMapperAndMediatR(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        return services;
    }
    
    public static IServiceCollection AddFluentEmailAndSmtpSender(this IServiceCollection services)
    {
        services.AddFluentEmail(EnvConfig.SenderEmail, EnvConfig.SenderName)
            .AddSmtpSender(EnvConfig.EmailHost, EnvConfig.EmailPort);

        return services;
    }
    

    public static void AddCustomLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    }
}