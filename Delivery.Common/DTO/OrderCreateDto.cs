namespace Delivery.Common.DTO; 

/// <summary>
/// DTO for creating order
/// </summary>
public class OrderCreateDto {
    /// <summary>
    /// Address of the order
    /// </summary>
    public String? Address { get; set; }
    /// <summary>
    /// Time when order should be delivered
    /// </summary>
    public DateTime DeliveryTime { get; set; }
    /// <summary>
    /// Customer comment to order
    /// </summary>
    public String? Comment { get; set; }
}