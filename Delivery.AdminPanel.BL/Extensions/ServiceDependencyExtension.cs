using Delivery.AdminPanel.BL.Services;
using Delivery.AuthAPI.DAL;
using Delivery.BackendAPI.DAL;
using Delivery.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.AdminPanel.BL.Extensions;
/// <summary>
/// Service dependency extension
/// </summary>
public static class ServiceDependencyExtension {
    /// <summary>
    /// Add AdminPanel BL service dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAdminPanelBlServiceDependencies(this IServiceCollection services) {
        services.AddDbContext<AuthDbContext>();
        services.AddDbContext<BackendDbContext>();
        services.AddScoped<IAdminPanelAccountService, AdminPanelAccountService>();
        services.AddScoped<IAdminPanelRestaurantService, AdminPanelRestaurantService>();
        services.AddScoped<IAdminPanelUserService, AdminPanelUserService>();
        services.AddAutoMapper(typeof(MappingProfiles.MappingProfile));
        return services;
    }
}