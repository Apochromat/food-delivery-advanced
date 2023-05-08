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
    public string Name { get; set; } = "";
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
    public String Address { get; set; } = "";
    /// <summary>
    /// List of menus in restaurant
    /// </summary>
    public required List<Menu> Menus { get; set; } = new ();
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
    public List<Guid> Cooks { get; set; } = new ();

    /// <summary>
    /// List of managers in restaurant
    /// </summary>
    public List<Guid> Managers { get; set; } = new ();
}