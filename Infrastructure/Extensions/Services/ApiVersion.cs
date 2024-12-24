using Asp.Versioning;

namespace vsa_journey.Infrastructure.Extensions.Services;

public static class ApiVersion
{
    public static IServiceCollection AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader =
                ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("X-Api-Version"));
        }).AddMvc().AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}