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
    public String? Name { get; set; }
}