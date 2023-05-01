using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Delivery.Notification.DAL;

/// <summary>
/// Create database context for design time
/// </summary>
public class DesignTimeNotificationDbContextFactory : IDesignTimeDbContextFactory<NotificationDbContext> {
    /// <summary>
    /// Create database context
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public NotificationDbContext CreateDbContext(string[] args) {
        var builder = new DbContextOptionsBuilder<NotificationDbContext>();
        var connectionString = "Host=localhost;Database=delivery-notification-db;Username=postgres;Password=postgres";
        return new NotificationDbContext(builder.Options, connectionString);
    }
}