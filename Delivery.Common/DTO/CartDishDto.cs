namespace Delivery.Common.DTO; 

/// <summary>
/// Data transfer object for dish in cart
/// </summary>
public class CartDishDto {
    DishShortDto? Dish { get; set; }
    int Amount { get; set; }
}