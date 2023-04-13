namespace Delivery.Common.DTO; 

/// <summary>
/// Data transfer object for cart
/// </summary>
public class CartDto {
    List<CartDishDto> Dishes { get; set; } = new List<CartDishDto>();
    int TotalPrice { get; set; }
    RestaurantShortDto? Restaurant { get; set; }
}