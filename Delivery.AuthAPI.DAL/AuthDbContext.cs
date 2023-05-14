using Delivery.AuthAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.AuthAPI.DAL;

/// <summary>
/// Auth database context
/// </summary>
public class AuthDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> {
    
    /// <summary>
    /// Users table
    /// </summary>
    public new DbSet<User> Users { get; set; } = null!; // Late initialization

    /// <summary>
    /// Customers table
    /// </summary>
    public DbSet<Customer> Customers { get; set; } = null!; // Late initialization

    /// <summary>
    /// Couriers table
    /// </summary>
    public DbSet<Courier> Couriers { get; set; } = null!; // Late initialization

    /// <summary>
    /// Managers table
    /// </summary>
    public DbSet<Manager> Managers { get; set; } = null!; // Late initialization

    /// <summary>
    /// Cooks table
    /// </summary>
    public DbSet<Cook> Cooks { get; set; } = null!; // Late initialization

    /// <summary>
    /// Devices table
    /// </summary>
    public DbSet<Device> Devices { get; set; } = null!; // Late initialization

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
}