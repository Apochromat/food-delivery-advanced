using Delivery.AuthAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delivery.AuthAPI.DAL; 

public class AuthDbContext : IdentityDbContext<User, Role, Guid> {
    public override DbSet<User> Users { get; set; }
    public override DbSet<Role> Roles { get; set; }
    public DbSet<Customer> Customers { get; set; }

    /// <inheritdoc />
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=auth-db;Username=postgres;Password=postgres");
    }
}