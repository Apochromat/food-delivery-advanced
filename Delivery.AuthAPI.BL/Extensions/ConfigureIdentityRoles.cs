using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.AuthAPI.BL.Extensions; 

public static class ConfigureIdentityRoles {
    public static async Task ConfigureIdentityAsync(this WebApplication app) {
        using var serviceScope = app.Services.CreateScope();
        
        // Migrate database
        var context = serviceScope.ServiceProvider.GetService<AuthDbContext>();
        if (context == null) {
            throw new ArgumentNullException(nameof(context));
        }
        await context.Database.MigrateAsync();
        
        var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
        if (userManager == null) {
            throw new ArgumentNullException(nameof(userManager));
        }
        var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
        if (roleManager == null) {
            throw new ArgumentNullException(nameof(roleManager));
        }
        
        var config = app.Configuration.GetSection("DefaultUsersConfig");

        // Try to create Roles
        foreach (var roleName in Enum.GetValues(typeof(RoleType))) {
            if (roleName == null) {
                throw new ArgumentNullException(nameof(roleName));
            }
            var role = await roleManager.FindByNameAsync(roleName.ToString() ?? "");
            if (role == null) {
                var roleResult =
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName.ToString() ?? ""));
                if (!roleResult.Succeeded) {
                    throw new InvalidOperationException($"Unable to create {roleName} role.");
                }

                role = await roleManager.FindByNameAsync(roleName.ToString() ?? "");
            }

            if (role == null || role.Name == null) {
                throw new ArgumentNullException(nameof(role));
            }
        }

        // Try to create Administrator user
        var adminUser = await userManager.FindByEmailAsync(config["AdminEmail"] ?? "");
        if (adminUser == null) {
            var userResult = await userManager.CreateAsync(new User {
                FullName = config["AdminFullName"],
                UserName = config["AdminUserName"],
                Email = config["AdminEmail"],
                JoinedAt = DateTime.Now.ToUniversalTime(),
                BirthDate = DateTime.Today.ToUniversalTime()
            }, config["AdminPassword"] ?? "");
            if (!userResult.Succeeded) {
                throw new InvalidOperationException($"Unable to create administrator user");
            }
            
            adminUser = await userManager.FindByNameAsync(config["AdminUserName"] ?? "");
        }

        if (adminUser == null) {
            throw new ArgumentNullException(nameof(adminUser));
        }

        if (!await userManager.IsInRoleAsync(adminUser, ApplicationRoleNames.Administrator)) {
            await userManager.AddToRoleAsync(adminUser, ApplicationRoleNames.Administrator);
        }
    }
}
