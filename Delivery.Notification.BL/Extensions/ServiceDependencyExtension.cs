using Delivery.Common.Interfaces;
using Delivery.Notification.BL.Services;
using Delivery.Notification.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Notification.BL.Extensions; 

/// <summary>
/// Extension for notification service dependencies.
/// </summary>
public static class ServiceDependencyExtension {
    /// <summary>
    /// Add notification service dependencies.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddNotificationServiceDependencies(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<NotificationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("NotificationDatabasePostgres")));
        services.AddHostedService<RabbitMqService>();
        services.AddScoped<IConnectionManagerService, ConnectionManagerService>();
        return services;
    }
}