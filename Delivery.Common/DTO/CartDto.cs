namespace Delivery.Common.DTO;

/// <summary>
/// Data transfer object for cart
/// </summary>
public class CartDto {
    /// <summary>
    /// List of dishes in cart
    /// </summary>
    public required List<CartDishDto> Dishes { get; set; } = new List<CartDishDto>();

    /// <summary>
    /// Total price of dishes in cart
    /// </summary>
    public required decimal TotalPrice { get; set; }

    /// <summary>
    /// Information about restaurant
    /// </summary>
    public required RestaurantShortDto Restaurant { get; set; }
}