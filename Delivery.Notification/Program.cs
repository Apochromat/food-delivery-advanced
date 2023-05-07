using Delivery.Common.Extensions;
using Delivery.Common.Interfaces;
using Delivery.Notification.BL.Extensions;
using Delivery.Notification.Hubs;
using Delivery.Notification.Services;
using Microsoft.AspNetCore.SignalR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add Cors Policy
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        policy => {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(hostName => true);
        });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddJwtAuthorisation();

// Add Notification dependencies
builder.Services.AddScoped<INotificationService, NotificationService>();

// Add Quartz
builder.Services.ConfigureQuartz();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Connect SignalR
builder.Services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();
builder.Services.AddSignalR();

builder.Services.AddNotificationServiceDependencies(builder.Configuration);

var app = builder.Build();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/api/notifications");

app.Run();