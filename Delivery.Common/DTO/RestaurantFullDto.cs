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
    public string? Name { get; set; }
    /// <summary>
    /// Is restaurant archived
    /// </summary>
    public Boolean IsArchived { get; set; }
    /// <summary>
    /// List of menus in restaurant
    /// </summary>
    public List<MenuShortDto>? Menus { get; set; }
}
