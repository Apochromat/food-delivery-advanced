namespace Delivery.Common.DTO;

/// <summary>
/// Data transfer object for dish in cart
/// </summary>
public class CartDishDto {
    /// <summary>
    /// Dish
    /// </summary>
    public required DishShortDto Dish { get; set; }

    /// <summary>
    /// Dish amount
    /// </summary>
    public int Amount { get; set; }
}