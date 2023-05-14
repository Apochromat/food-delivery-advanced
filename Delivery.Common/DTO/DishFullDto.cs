using Delivery.Common.Enums;

namespace Delivery.Common.DTO;

/// <summary>
/// Dish DTO info about dish
/// </summary>
public class DishFullDto {
    /// <summary>
    /// Dish Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the dish
    /// </summary>
    public required string Name { get; set; }

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
    public required string ImageUrl { get; set; }

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
    public required List<DishCategory> DishCategories { get; set; }

    /// <summary>
    /// Is dish vegetarian
    /// </summary>
    public Boolean IsVegetarian { get; set; }
}