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
using vsa_journey.Infrastructure.Extensions.ApplicationBuilder;
using vsa_journey.Infrastructure.Extensions.Services;


var builder = WebApplication.CreateBuilder(args);
var databaseUrl = EnvConfig.DatabaseUrl;


var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 36));

{
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IApiResponse, ApiResponse>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(databaseUrl, mySqlServerVersion));

    builder.Services.AddIdentity<User, IdentityRole<Guid>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddCustomCookieAuthentication(builder.Services.BuildServiceProvider());
    builder.Services.AddAuthorization(options => options.AddCustomPolicies());
    builder.Services.AddJwtBearerAuthentication();
    builder.Services.AddApiVersion();

    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));
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

    app.UseAuthentication();
    
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
