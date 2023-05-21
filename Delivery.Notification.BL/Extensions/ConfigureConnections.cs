using Delivery.Notification.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Notification.BL.Extensions;

/// <summary>
/// Create default roles and users
/// </summary>
public static class ConfigureConnections {
    /// <summary>
    /// Create default roles and administrator user
    /// </summary>
    /// <param name="app"></param>
    public static async Task ConfigureConnectionsAsync(this WebApplication app) {
        using var serviceScope = app.Services.CreateScope();

        // Migrate database
        var context = serviceScope.ServiceProvider.GetService<NotificationDbContext>();
        if (context == null) {
            throw new ArgumentNullException(nameof(context));
        }

        await context.Database.MigrateAsync();
        
        context.Connections.RemoveRange(context.Connections);
        await context.SaveChangesAsync();
    }
}