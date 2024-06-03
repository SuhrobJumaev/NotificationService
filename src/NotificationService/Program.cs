using Microsoft.Extensions.Configuration;
using NotificationService.Factories;
using NotificationService.Helpers;
using NotificationService.Interfaces;
using NotificationService.Services;
using NotificationService.Repositories;
using FluentValidation;
using NotificationService.Middlewares;
using NotificationService.Jobs;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using NotificationService.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using NotificationService.Health;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1.0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new MediaTypeApiVersionReader(Utils.ApiVersionTag);
}).AddMvc().AddApiExplorer();


builder.Services.AddControllers();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(x => x.OperationFilter<SwaggerDefaultValues>());


builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
           new NpgsqlConnectionFactory(config.GetSection(Utils.ConnectionStringsSectionName)[Utils.DefaultConnectionKeyName]!));

builder.Services.AddSingleton<DbInitializer>();

builder.Services.AddTransient<INotificationEventService, NotificationEventService>();
builder.Services.AddScoped<INotificationEventRepository, NotificationEventRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();
builder.Services.AddHostedService<NotificationBackgroundJob>();
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>(Utils.HealthCheckName);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    foreach (var description in app.DescribeApiVersions())
    {
        x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName);

    }
});

app.MapHealthChecks(Utils.HealthCheckEndpoint);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
