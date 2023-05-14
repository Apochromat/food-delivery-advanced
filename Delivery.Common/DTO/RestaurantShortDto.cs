namespace Delivery.Common.DTO;

/// <summary>
/// Restaurant DTO to short info about restaurant
/// </summary>
public class RestaurantShortDto {
    /// <summary>
    /// Restaurant id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Restaurant name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Image of restaurant (URL)
    /// </summary>
    public required string SmallImage { get; set; }
}