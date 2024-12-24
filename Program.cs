using Serilog;
using vsa_journey.Infrastructure.Extensions.Services;
using vsa_journey.Infrastructure.Extensions.ApplicationBuilder;
using vsa_journey.Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddCustomServices();
    builder.Services.AddControllers();
    builder.Services.AddSwaggerrAndApiVersioning();
    builder.Services.AddPersistence();
    builder.Services.AddIdentityServices();
    builder.Services.AddCustomAuthentication();
    builder.Services.AddAutoMapperAndMediatR();
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
