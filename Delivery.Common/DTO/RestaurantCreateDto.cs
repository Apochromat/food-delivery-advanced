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
    /// <summary>
    /// Small Image of restaurant (URL)
    /// </summary>
    [Required]
    public String SmallImage { get; set; } = "";
    /// <summary>
    /// Big image of restaurant (URL)
    /// </summary>
    [Required]
    public String BigImage { get; set; } = "";
    /// <summary>
    /// Short description of restaurant
    /// </summary>
    public String? Description { get; set; }
    /// <summary>
    /// Restaurant address
    /// </summary>
    [Required]
    public String Address { get; set; } = "";
}