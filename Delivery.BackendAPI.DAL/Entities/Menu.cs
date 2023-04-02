namespace Delivery.BackendAPI.DAL.Entities; 

/// <summary>
/// Menu entity
/// </summary>
public class Menu {
    /// <summary>
    /// Menu Identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Name of the menu
    /// </summary>
    public string? Name { get; set; }
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
    public List<Dish>? Dishes { get; set; }
    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Update date
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}