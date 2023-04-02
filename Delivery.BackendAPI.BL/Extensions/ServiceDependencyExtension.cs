using Delivery.BackendAPI.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.BackendAPI.BL.Extensions;
/// <summary>
/// Service dependency extension
/// </summary>
public static class ServiceDependencyExtension {
    /// <summary>
    /// Add BackendAPI BL service dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBackendBlServiceDependencies(this IServiceCollection services) {
        services.AddDbContext<BackendDbContext>();
        return services;
    }
}