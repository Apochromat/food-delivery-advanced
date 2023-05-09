using System.Text.Json.Serialization;

namespace Delivery.Common.DTO; 

/// <summary>
/// DTO of dish with short information
/// </summary>
public class DishOrderDto {
    /// <summary>
    /// Dish Identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Name of the dish
    /// </summary>
    [JsonPropertyName("name")]
    public string? ArchivedDishName { get; set; }
    /// <summary>
    /// Description of the dish
    /// </summary>
    [JsonPropertyName("description")]
    public string? ArchivedDishDescription { get; set; }
    /// <summary>
    /// Dish price
    /// </summary>
    [JsonPropertyName("price")]
    public decimal ArchivedDishPrice { get; set; }
    /// <summary>
    /// Image url
    /// </summary>
    [JsonPropertyName("imageUrl")]
    public string? ArchivedDishImageUrl { get; set; }
    /// <summary>
    /// Calculated rating
    /// </summary>
    public decimal CalculatedRating { get; set; }
    /// <summary>
    /// Is dish vegetarian
    /// </summary>
    public Boolean IsVegetarian { get; set; }
    /// <summary>
    /// Dish amount
    /// </summary>
    public int Amount { get; set; }
}