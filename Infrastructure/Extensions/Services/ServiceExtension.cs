using MediatR;
using Serilog;
using vsa_journey.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Application.Responses;
using vsa_journey.Application.Behaviours;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Features.Authentication.Policies;
using vsa_journey.Features.Authentication.Extensions;


namespace vsa_journey.Infrastructure.Extensions.Services;

public static class ServiceExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IApiResponse, ApiResponse>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
    }

    public static void AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddCustomCookieAuthentication(services.BuildServiceProvider());
        services.AddJwtBearerAuthentication();
        services.AddAuthorization(options => options.AddCustomPolicies());
    }


    public static void AddSwaggerrAndApiVersioning(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCustomApiVersion();
    }

    public static void AddPersistence(this IServiceCollection services)
    {
        var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 36));
        services.AddDbContext<AppDbContext>(options => options.UseMySql(EnvConfig.DatabaseUrl, mySqlServerVersion));
    }

    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddAutoMapperAndMediatR(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));
    }

    public static void AddCustomLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    }
}