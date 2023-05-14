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
    /// Number of the order
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Total price of the order
    /// </summary>
    public decimal TotalPrice { get; set; }
}