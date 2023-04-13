using Delivery.AuthAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Delivery.AuthAPI.DAL;

/// <summary>
/// Auth database context
/// </summary>
public class AuthDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> {
    private readonly IConfiguration? _configuration;

    private readonly String? _connection =
        "Host=localhost;Database=delivery-auth-db;Username=postgres;Password=postgres";

    /// <summary>
    /// Users table
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Customers table
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Couriers table
    /// </summary>
    public DbSet<Courier> Couriers { get; set; }

    /// <summary>
    /// Managers table
    /// </summary>
    public DbSet<Manager> Managers { get; set; }

    /// <summary>
    /// Cooks table
    /// </summary>
    public DbSet<Cook> Cooks { get; set; }

    /// <summary>
    /// Devices table
    /// </summary>
    public DbSet<Device> Devices { get; set; }

    /// <inheritdoc />
    public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options) {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.Entity<User>().HasOne(u => u.Customer).WithOne(c => c.User).HasForeignKey<Customer>();
        builder.Entity<User>().HasOne(u => u.Courier).WithOne(c => c.User).HasForeignKey<Courier>();
        builder.Entity<User>().HasOne(u => u.Manager).WithOne(c => c.User).HasForeignKey<Manager>();
        builder.Entity<User>().HasOne(u => u.Cook).WithOne(c => c.User).HasForeignKey<Cook>();
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (_configuration == null) {
            optionsBuilder.UseNpgsql(_connection,
                b => b.MigrationsAssembly("Delivery.AuthAPI.DAL"));
        }
        else {
            optionsBuilder.UseNpgsql(_configuration?.GetConnectionString("AuthDatabasePostgres"),
                b => b.MigrationsAssembly("Delivery.AuthAPI.DAL"));
        }
    }
}