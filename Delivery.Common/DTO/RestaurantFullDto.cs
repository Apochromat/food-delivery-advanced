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
    /// Small image of restaurant (URL)
    /// </summary>
    public String SmallImage { get; set; } = "";
    /// <summary>
    /// Big image of restaurant (URL)
    /// </summary>
    public String BigImage { get; set; } = "";
    /// <summary>
    /// Short description of restaurant
    /// </summary>
    public String? Description { get; set; }
    /// <summary>
    /// Restaurant address
    /// </summary>
    public String? Address { get; set; }
    /// <summary>
    /// List of menus in restaurant
    /// </summary>
    public List<MenuShortDto>? Menus { get; set; }
}
