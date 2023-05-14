using Delivery.AuthAPI.BL.Services;
using Delivery.AuthAPI.DAL;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthBlServiceDependencies(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<AuthDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("AuthDatabasePostgres")));

    services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddAutoMapper(typeof(MappingProfiles.MappingProfile));
        return services;
    }
}