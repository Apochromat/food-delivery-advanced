using System.ComponentModel;

namespace Delivery.Common.DTO; 

/// <summary>
/// Message DTO
/// </summary>
public class MessageDto {
    /// <summary>
    /// User identifier
    /// </summary>
    [DisplayName("receiverId")]
    public Guid ReceiverId { get; set; }
    /// <summary>
    /// Message title
    /// </summary>
    [DisplayName("title")]
    public required string Title { get; set; }
    /// <summary>
    /// Message text
    /// </summary>
    [DisplayName("text")]
    public string Text { get; set; } = string.Empty;
    /// <summary>
    /// Message creation date
    /// </summary>
    [DisplayName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Message delivery date
    /// </summary>
    [DisplayName("deliveredAt")]
    public DateTime? DeliveredAt { get; set; }
}