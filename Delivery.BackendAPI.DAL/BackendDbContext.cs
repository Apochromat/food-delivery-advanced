using Delivery.BackendAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BackendAPI.DAL;

/// <summary>
/// Backend database context
/// </summary>
public class BackendDbContext : DbContext {
    /// <summary>
    /// Restaurants table
    /// </summary>
    public DbSet<Restaurant> Restaurants { get; set; } = null!; // Late initialization

    /// <summary>
    /// Menus table
    /// </summary>
    public DbSet<Menu> Menus { get; set; } = null!; // Late initialization

    /// <summary>
    /// Dishes table
    /// </summary>
    public DbSet<Dish> Dishes { get; set; } = null!; // Late initialization

    /// <summary>
    /// Orders table
    /// </summary>
    public DbSet<Order> Orders { get; set; } = null!; // Late initialization

    /// <summary>
    /// Dishes in order table
    /// </summary>
    public DbSet<DishInCart> DishesInCart { get; set; } = null!; // Late initialization

    /// <summary>
    /// Ratings table
    /// </summary>
    public DbSet<Rating> Ratings { get; set; } = null!; // Late initialization

    /// <inheritdoc />
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) {
    }
}