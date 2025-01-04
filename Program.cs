using Serilog;
using vsa_journey.Infrastructure.Middlewares;
using vsa_journey.Infrastructure.Extensions.Services;
using vsa_journey.Infrastructure.Extensions.ApplicationBuilder;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddCustomServices()
        .AddSwaggerAndApiVersioning()
        .AddPersistence()
        .AddIdentityServices()
        .AddCustomAuthentication()
        .AddAutoMapperAndMediatR()
        .AddFluentEmailAndSmtpSender()
        .AddHttpContextAccessor()
        .AddControllers();
    
    builder.Host.AddCustomLogging();
}

{   
    var app = builder.Build();

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
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
