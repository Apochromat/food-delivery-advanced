namespace Delivery.AuthAPI.DAL.Entities;

/// <summary>
/// Courier entity
/// </summary>
public class Courier {
    /// <summary>
    /// Courier`s id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Link to user
    /// </summary>
    public required User User { get; set; }
}