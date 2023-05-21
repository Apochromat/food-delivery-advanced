using Delivery.BackendAPI.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.BackendAPI.BL.Extensions;

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
        var context = serviceScope.ServiceProvider.GetService<BackendDbContext>();
        if (context == null) {
            throw new ArgumentNullException(nameof(context));
        }

        await context.Database.MigrateAsync();
    }
}