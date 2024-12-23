using System.Text;
using MediatR;
using Serilog;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using vsa_journey.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using vsa_journey.Domain.Entities.User;
using vsa_journey.Application.Behaviours;
using vsa_journey.Features.Authentication.Policies;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Repositories;
using vsa_journey.Infrastructure.Extensions.ApplicationBuilder;


var builder = WebApplication.CreateBuilder(args);
var databaseUrl = EnvConfig.DatabaseUrl;


var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 36));

{
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(databaseUrl, mySqlServerVersion));

    builder.Services.AddIdentity<User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAuthorization(options => options.AddCustomPolicies());

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = EnvConfig.IsProduction;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = EnvConfig.ValidIssuer,
            ValidAudience = EnvConfig.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("")),
        };
    });

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader =
            ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("X-Api-Version"));
    }).AddMvc().AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddMediatR(configuration =>
    {
        configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });

    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

}


{   
    var app = builder.Build();
    
    await app.UseSeedingAsync();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
