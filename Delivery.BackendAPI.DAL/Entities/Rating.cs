namespace Delivery.BackendAPI.DAL.Entities; 

/// <summary>
/// Rating entity
/// </summary>
public class Rating {
    /// <summary>
    /// Rating identifier
    /// </summary>
    public required Guid Id { get; set; }
    /// <summary>
    /// Identifier of customer
    /// </summary>
    public required Guid CustomerId { get; set; }
    /// <summary>
    /// Dish of the rating
    /// </summary>
    public required Dish Dish { get; set; }
    /// <summary>
    /// Rating value
    /// </summary>
    public int Value { get; set; }
}