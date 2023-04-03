using Delivery.AuthAPI.BL.Services;
using Delivery.AuthAPI.DAL;
using Delivery.Common.Interfaces;
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
        services.AddScoped<IAuthService, AuthService>();
        services.AddAutoMapper(typeof(MappingProfiles.MappingProfile));
        return services;
    }
}