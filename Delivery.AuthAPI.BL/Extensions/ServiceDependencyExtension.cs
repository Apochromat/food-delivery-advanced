using Delivery.AuthAPI.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.AuthAPI.BL.Extensions;
/// <summary>
/// Service dependency extension
/// </summary>
public static class ServiceDependencyExtension {
    /// <summary>
    /// Add BackendAPI BL service dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthBlServiceDependencies(this IServiceCollection services) {
        services.AddDbContext<AuthDbContext>();
        services.AddAutoMapper(typeof(MappingProfiles.MappingProfile));
        return services;
    }
}