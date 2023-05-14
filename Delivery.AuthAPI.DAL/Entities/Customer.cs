namespace Delivery.AuthAPI.DAL.Entities;

/// <summary>
/// Customer entity
/// </summary>
public class Customer {
    /// <summary>
    /// Customer`s id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Link to user
    /// </summary>
    public required User User { get; set; }

    /// <summary>
    /// Customer`s address 
    /// </summary>
    public string? Address { get; set; }
}