using Delivery.AuthAPI.DAL;
using Delivery.BackendAPI.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.AdminPanel.BL.Extensions;

/// <summary>
/// Create default roles and users
/// </summary>
public static class UpdateDatabaseExtension {
    /// <summary>
    /// Create default roles and administrator user
    /// </summary>
    /// <param name="app"></param>
    public static async Task UpdateDatabaseAsync(this WebApplication app) {
        using var serviceScope = app.Services.CreateScope();

        // Migrate database
        var backendDbContext = serviceScope.ServiceProvider.GetService<BackendDbContext>();
        if (backendDbContext == null) {
            throw new ArgumentNullException(nameof(backendDbContext));
        }

        await backendDbContext.Database.MigrateAsync();
        
        var authDbContext = serviceScope.ServiceProvider.GetService<AuthDbContext>();
        if (authDbContext == null) {
            throw new ArgumentNullException(nameof(authDbContext));
        }

        await authDbContext.Database.MigrateAsync();
    }
}