using System.ComponentModel.DataAnnotations;
using Delivery.Common.Enums;

namespace Delivery.Common.DTO;

/// <summary>
/// Dish create dto
/// </summary>
public class DishCreateDto {
    /// <summary>
    /// Name of the dish
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// Description of the dish
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Dish price
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Image url
    /// </summary>
    [Required]
    public required string ImageUrl { get; set; }

    /// <summary>
    /// Categories of the dish
    /// </summary>
    public List<DishCategory> DishCategories { get; set; } = new List<DishCategory>();

    /// <summary>
    /// Is dish vegetarian
    /// </summary>
    public Boolean IsVegetarian { get; set; } = false;
}