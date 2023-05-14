namespace Delivery.Common.DTO;

/// <summary>
/// DTO of restaurant with full information
/// </summary>
public class RestaurantFullDto {
    /// <summary>
    /// Restaurant Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the restaurant
    /// </summary>
    public string Name { get; set; } = null!; // Late initialization

    /// <summary>
    /// Is restaurant archived
    /// </summary>
    public Boolean IsArchived { get; set; }

    /// <summary>
    /// Small image of restaurant (URL)
    /// </summary>
    public string SmallImage { get; set; } = null!; // Late initialization

    /// <summary>
    /// Big image of restaurant (URL)
    /// </summary>
    public string BigImage { get; set; } = null!; // Late initialization

    /// <summary>
    /// Short description of restaurant
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Restaurant address
    /// </summary>
    public string Address { get; set; } = null!; // Late initialization
}