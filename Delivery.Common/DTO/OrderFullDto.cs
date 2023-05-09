using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// DTO of order with full information
/// </summary>
public class OrderFullDto {
    /// <summary>
    /// Order Identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Number of the order in "ORD-88005553535-0001" format
    /// </summary>
    public String? Number { get; set; }
    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// Customer comment to order
    /// </summary>
    public String? Comment { get; set; }
    /// <summary>
    /// Time when order should be delivered
    /// </summary>
    public DateTime DeliveryTime { get; set; }
    /// <summary>
    /// Time when order was created
    /// </summary>
    public DateTime OrderTime { get; set; }
    /// <summary>
    /// Time when order was delivered
    /// </summary>
    public DateTime DeliveredTime { get; set; }
    /// <summary>
    /// Total price of the order
    /// </summary>
    public decimal TotalPrice { get; set; }
    /// <summary>
    /// Address of the order
    /// </summary>
    public String? Address { get; set; }
    /// <summary>
    /// Order dishes
    /// </summary>
    public List<DishShortDto> Dishes { get; set; } = new();
}