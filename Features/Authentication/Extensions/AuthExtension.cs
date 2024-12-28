using System.Text;
using vsa_journey.Utils;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using vsa_journey.Application.Responses;

namespace vsa_journey.Features.Authentication.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = EnvConfig.IsProduction;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
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
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = apiResponse.Unauthorized();

                return context.Response.WriteAsJsonAsync(response);
            };

            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = apiResponse.Forbidden();

                return context.Response.WriteAsJsonAsync(response);
            };

        });

        return services;
    }
}   