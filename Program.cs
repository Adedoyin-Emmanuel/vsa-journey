using MediatR;
using Serilog;
using Asp.Versioning;
using vsa_journey.Utils;
using Microsoft.EntityFrameworkCore;
using vsa_journey.Application.Behaviours;
using vsa_journey.Infrastructure.Persistence;
using vsa_journey.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);
var databaseUrl = EnvConfig.DatabaseUrl;


var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 36));

{
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(databaseUrl, mySqlServerVersion));

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
    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration);
    });
}


{
    var app = builder.Build();      
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
