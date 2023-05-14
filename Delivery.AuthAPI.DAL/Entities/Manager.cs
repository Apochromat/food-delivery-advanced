namespace Delivery.AuthAPI.DAL.Entities;

/// <summary>
/// Manager entity
/// </summary>
public class Manager {
    /// <summary>
    /// Manager`s id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Link to user
    /// </summary>
    public required User User { get; set; }
}