using Delivery.Notification.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Delivery.Notification.DAL;

/// <summary>
/// Notification database context
/// </summary>
public class NotificationDbContext : DbContext {
    private readonly IConfiguration? _configuration;
    private readonly String? _connection;
    /// <summary>
    /// Restaurants table
    /// </summary>
    public DbSet<Message> Messages { get; set; }

    /// <inheritdoc />
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options, IConfiguration configuration) : base(options) {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options, String connection) : base(options) {
        _connection = connection;
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (_configuration == null) {
            optionsBuilder.UseNpgsql(_connection,
                b => b.MigrationsAssembly("Delivery.Notification.DAL"));
        }
        else {
            optionsBuilder.UseNpgsql(_configuration?.GetConnectionString("NotificationDatabasePostgres"),
                b => b.MigrationsAssembly("Delivery.Notification.DAL"));
        }
    }
}