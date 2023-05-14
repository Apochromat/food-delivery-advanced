namespace Delivery.BackendAPI.DAL.Entities;

/// <summary>
/// Entity for dish in cart
/// </summary>
public class DishInCart {
    /// <summary>
    /// Dish in cart identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Dish in cart
    /// </summary>
    public required Dish Dish { get; set; }

    /// <summary>
    /// Amount of dishes in cart
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Identifier of customer
    /// </summary>
    public required Guid CustomerId { get; set; }

    /// <summary>
    /// Restaurant of the dish
    /// </summary>
    public required Restaurant Restaurant { get; set; }
}