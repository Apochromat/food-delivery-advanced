using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO; 

/// <summary>
/// DTO for updating restaurant
/// </summary>
public class RestaurantEditDto {
    /// <summary>
    /// Name of the restaurant
    /// </summary>
    [Required]
    public string Name { get; set; } = "";
    /// <summary>
    /// Image of restaurant (URL)
    /// </summary>
    [Required]
    public String SmallImage { get; set; } = "";
    /// <summary>
    /// Image of restaurant (URL)
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