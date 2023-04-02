using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO; 
/// <summary>
/// DTO for creating new restaurant
/// </summary>
public class RestaurantCreateDto {
    /// <summary>
    /// Name of restaurant
    /// </summary>
    [Required]
    public String Name { get; set; } = "";
}