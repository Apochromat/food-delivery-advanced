using Delivery.Notification.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Notification.DAL;

/// <summary>
/// Notification database context
/// </summary>
public class NotificationDbContext : DbContext {
    /// <summary>
    /// Messages table
    /// </summary>
    public DbSet<Message> Messages { get; set; } = null!; // Late initialization

    /// <summary>
    /// Connections table
    /// </summary>
    public DbSet<Connection> Connections { get; set; } = null!; // Late initialization

    /// <inheritdoc />
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) {
    }
}