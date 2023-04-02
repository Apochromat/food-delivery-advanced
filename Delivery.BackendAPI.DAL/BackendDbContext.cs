using Delivery.BackendAPI.DAL.Entities;
using Delivery.BackendAPI.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Delivery.BackendAPI.DAL;

/// <summary>
/// Backend database context
/// </summary>
public class BackendDbContext : DbContext {
    private readonly IConfiguration? _configuration;
    private readonly String? _connection;
    /// <summary>
    /// Restaurants table
    /// </summary>
    public DbSet<Restaurant> Restaurants { get; set; }

    /// <summary>
    /// Menus table
    /// </summary>
    public DbSet<Menu> Menus { get; set; }

    /// <summary>
    /// Dishes table
    /// </summary>
    public DbSet<Dish> Dishes { get; set; }

    /// <summary>
    /// Orders table
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Dishes in order table
    /// </summary>
    public DbSet<DishInCart> DishesInCart { get; set; }

    /// <summary>
    /// Ratings table
    /// </summary>
    public DbSet<Rating> Ratings { get; set; }

    /// <inheritdoc />
    public BackendDbContext(DbContextOptions<BackendDbContext> options, IConfiguration configuration) : base(options) {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public BackendDbContext(DbContextOptions<BackendDbContext> options, String connection) : base(options) {
        _connection = connection;
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (_configuration == null) {
            optionsBuilder.UseNpgsql(_connection,
                b => b.MigrationsAssembly("Delivery.BackendAPI.DAL"));
        }
        else {
            optionsBuilder.UseNpgsql(_configuration?.GetConnectionString("BackendDatabasePostgres"),
                b => b.MigrationsAssembly("Delivery.BackendAPI.DAL"));
        }
    }
    
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Seed();
    }
}