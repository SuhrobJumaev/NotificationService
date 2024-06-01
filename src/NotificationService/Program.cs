using Microsoft.Extensions.Configuration;
using NotificationService.Factories;
using NotificationService.Helpers;
using NotificationService.Interfaces;
using NotificationService.Services;
using NotificationService.Repositories;
using FluentValidation;
using NotificationService.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
           new NpgsqlConnectionFactory(config.GetSection(Utils.ConnectionStringsSectionName)[Utils.DefaultConnectionKeyName]!));

builder.Services.AddSingleton<DbInitializer>();

builder.Services.AddTransient<INotificationEventService, NotificationEventService>();
builder.Services.AddScoped<INotificationEventRepository, NotificationEventRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
