using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO;

/// <summary>
/// DTO for creating order
/// </summary>
public class OrderCreateDto {
    /// <summary>
    /// Address of the order
    /// </summary>
    [Required]
    public required string Address { get; set; }

    /// <summary>
    /// Time when order should be delivered
    /// </summary>
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Customer comment to order
    /// </summary>
    public string? Comment { get; set; }
}