namespace Delivery.BackendAPI.DAL.Entities; 

/// <summary>
/// Restaurant entity
/// </summary>
public class Restaurant {
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
    public List<Menu>? Menus { get; set; }
    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Update date
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// List of cooks in restaurant
    /// </summary>
    public List<Guid>? Cooks { get; set; }
    /// <summary>
    /// List of managers in restaurant
    /// </summary>
    public List<Guid>? Managers { get; set; }
}