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
    public string Name { get; set; } = null!; // Late initialization

    /// <summary>
    /// Small Image of restaurant (URL)
    /// </summary>
    [Required]
    public string SmallImage { get; set; }  = null!; // Late initialization

    /// <summary>
    /// Big image of restaurant (URL)
    /// </summary>
    [Required]
    public string BigImage { get; set; }  = null!; // Late initialization

    /// <summary>
    /// Short description of restaurant
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Restaurant address
    /// </summary>
    [Required]
    public string Address { get; set; }  = null!; // Late initialization
}