using System.ComponentModel.DataAnnotations;
using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// DTO for changing order status
/// </summary>
public class OrderChangeStatusDto {
    /// <summary>
    /// New order status
    /// </summary>
    [Required]
    public OrderStatus Status { get; set; }
}