using Delivery.Common.Interfaces;
using Delivery.Notification.BL.Services;
using Delivery.Notification.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Notification.BL.Extensions; 

public static class ServiceDependencyExtension {
    public static IServiceCollection AddNotificationServiceDependencies(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<NotificationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("NotificationDatabasePostgres")));
        services.AddHostedService<RabbitMqService>();
        services.AddScoped<IConnectionManagerService, ConnectionManagerService>();
        return services;
    }
}