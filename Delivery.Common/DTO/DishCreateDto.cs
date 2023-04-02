using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// Dish create dto
/// </summary>
public class DishCreateDto {
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
    /// </summary>
    public string? ImageUrl { get; set; }
    /// <summary>
    /// Categories of the dish
    /// </summary>
    public List<DishCategory>? DishCategories { get; set; }
    /// <summary>
    /// Is dish vegetarian
    /// </summary>
    public Boolean IsVegetarian { get; set; }
}