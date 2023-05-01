using Delivery.Notification.BL.Services;
using Delivery.Notification.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Notification.BL.Extensions; 

public static class ServiceDependencyExtension {
    public static IServiceCollection AddNotificationServiceDependencies(this IServiceCollection services) {
        services.AddDbContext<NotificationDbContext>(ServiceLifetime.Singleton);
        services.AddHostedService<RabbitMqService>();
        return services;
    }
}