using Delivery.Notification.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Delivery.Notification.DAL;

/// <summary>
/// Notification database context
/// </summary>
public class NotificationDbContext : DbContext {
    /// <summary>
    /// Messages table
    /// </summary>
    public DbSet<Message> Messages { get; set; }
    /// <summary>
    /// Connections table
    /// </summary>
    public DbSet<Connection> Connections { get; set; }

    /// <inheritdoc />
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
}