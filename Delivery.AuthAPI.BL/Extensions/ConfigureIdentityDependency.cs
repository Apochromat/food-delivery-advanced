using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.AuthAPI.BL.Extensions;

/// <summary>
/// Extension methods for AuthAPI BL service Identity dependencies
/// </summary>
public static class ConfigureIdentityDependency {
    /// <summary>
    /// Add AuthAPI BL service Identity dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuthBlServiceIdentityDependencies(this IServiceCollection services) {
        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<User>>()
            .AddUserManager<UserManager<User>>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>();
        return services;
    }
}