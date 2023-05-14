using Delivery.AdminPanel.BL.Services;
using Delivery.AuthAPI.DAL;
using Delivery.BackendAPI.DAL;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAdminPanelBlServiceDependencies(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AuthDatabasePostgres")));
        services.AddDbContext<BackendDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("BackendDatabasePostgres")));
        services.AddScoped<IAdminPanelAccountService, AdminPanelAccountService>();
        services.AddScoped<IAdminPanelRestaurantService, AdminPanelRestaurantService>();
        services.AddScoped<IAdminPanelUserService, AdminPanelUserService>();
        services.AddAutoMapper(typeof(MappingProfiles.MappingProfile));
        return services;
    }
}