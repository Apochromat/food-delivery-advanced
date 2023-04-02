using Delivery.Common.Enums;

namespace Delivery.BackendAPI.DAL.Entities; 

/// <summary>
/// Dish entity
/// </summary>
public class Dish {
    /// <summary>
    /// Dish Identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Name of the dish
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Description of the dish
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Dish price
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Image url
    /// TODO: add image upload
    /// </summary>
    public string? ImageUrl { get; set; }
    /// <summary>
    /// Is dish archived
    /// </summary>
    public Boolean IsArchived { get; set; }
    /// <summary>
    /// Calculated rating
    /// </summary>
    public decimal CalculatedRating { get; set; }
    /// <summary>
    /// Categories of the dish
    /// </summary>
    public List<DishCategory>? DishCategories { get; set; }
    /// <summary>
    /// Is dish vegetarian
    /// </summary>
    public Boolean IsVegetarian { get; set; }
    /// <summary>
    /// Menus where dish is available
    /// </summary>
    public List<Menu?>? Menus { get; set; }
    /// <summary>
    /// Orders where dish is located
    /// </summary>
    public List<OrderDish>? Orders { get; set; }
    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Update date
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}