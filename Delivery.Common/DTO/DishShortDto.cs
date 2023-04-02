namespace Delivery.Common.DTO; 

/// <summary>
/// DTO of dish with short information
/// </summary>
public class DishShortDto {
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
    /// </summary>
    public string? ImageUrl { get; set; }
    /// <summary>
    /// Calculated rating
    /// </summary>
    public decimal CalculatedRating { get; set; }
    /// <summary>
    /// Is dish vegetarian
    /// </summary>
    public Boolean IsVegetarian { get; set; }
}