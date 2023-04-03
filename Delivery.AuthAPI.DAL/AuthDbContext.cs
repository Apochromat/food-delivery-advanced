using Delivery.AuthAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Delivery.AuthAPI.DAL; 

public class AuthDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> {
    private readonly IConfiguration? _configuration;
    private readonly String? _connection = "Host=localhost;Database=delivery-auth-db;Username=postgres;Password=postgres";
    
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }

    /// <inheritdoc />
    public AuthDbContext(DbContextOptions<AuthDbContext> options, IConfiguration configuration) : base(options) {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {
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