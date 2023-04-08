using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.AdminPanel.BL.Extensions; 

/// <summary>
/// Extension methods for AuthAPI BL service Identity dependencies
/// </summary>
public static class ConfigureIdentityDependency {
    /// <summary>
    /// Add AuthAPI BL service Identity dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAdminPanelBlServiceIdentityDependencies(this IServiceCollection services) {
        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>();
        return services;
    }
}