using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// DTO of order with short information
/// </summary>
public class OrderShortDto {
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
    /// Total price of the order
    /// </summary>
    public decimal TotalPrice { get; set; }
}