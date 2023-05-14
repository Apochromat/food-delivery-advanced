namespace Delivery.Common.DTO;

/// <summary>
/// Menu DTO with full information
/// </summary>
public class MenuFullDto {
    /// <summary>
    /// Menu Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the menu
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Indicates if menu is default (only one default menu can exist
    /// </summary>
    public Boolean IsDefault { get; set; }

    /// <summary>
    /// Is menu archived
    /// </summary>
    public Boolean IsArchived { get; set; }

    /// <summary>
    /// List of dishes in menu
    /// </summary>
    public List<DishShortDto> Dishes { get; set; } = new();
}