using System.Text;
using vsa_journey.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using vsa_journey.Application.Responses;

namespace vsa_journey.Features.Authentication.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IServiceProvider serviceProvider)
    {
        var apiResponse = serviceProvider.GetRequiredService<IApiResponse>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var response = apiResponse.Unauthorized();
                    await context.Response.WriteAsJsonAsync(response);

                    await context.Response.CompleteAsync();
                },
                
                OnForbidden = async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    
                    var response = apiResponse.Forbidden();
                    await context.Response.WriteAsJsonAsync(response);
                    
                    await context.Response.CompleteAsync();
                },
                
                OnTokenValidated = context =>
                {
                    Console.WriteLine($"IsProduction: {EnvConfig.IsProduction}");
                    Console.WriteLine($"ValidIssuer: {EnvConfig.ValidIssuer}");
                    Console.WriteLine($"ValidAudience: {EnvConfig.ValidAudience}");
                    Console.WriteLine($"JwtSecret: {EnvConfig.JwtSecret}");

                    Console.WriteLine("Token validated successfully.");
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                }
                
            };
            options.SaveToken = true;
            options.RequireHttpsMetadata = EnvConfig.IsProduction;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = EnvConfig.ValidIssuer,
                ValidAudience = EnvConfig.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvConfig.JwtSecret)),
            };
        });

        return services;
    }

    public static IServiceCollection AddCustomCookieAuthentication(this IServiceCollection services, IServiceProvider serviceProvider)
    {
        var apiResponse = serviceProvider.GetRequiredService<IApiResponse>();
        
        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = apiResponse.Unauthorized();

                await context.Response.WriteAsJsonAsync(response);

                await context.Response.CompleteAsync();
            };

            options.Events.OnRedirectToAccessDenied = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = apiResponse.Forbidden();

                await context.Response.WriteAsJsonAsync(response);
                
                await context.Response.CompleteAsync();
            };

        });

        return services;
    }
}   