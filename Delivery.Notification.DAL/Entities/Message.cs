using System.ComponentModel.DataAnnotations;

namespace Delivery.Notification.DAL.Entities; 

/// <summary>
/// Message entity
/// </summary>
public class Message {
    /// <summary>
    /// Message Identifier
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid ReceiverId { get; set; }
    /// <summary>
    /// Message title
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// Message text
    /// </summary>
    public string Text { get; set; } = string.Empty;
    /// <summary>
    /// Message creation date
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Message delivery date
    /// </summary>
    public DateTime? DeliveredAt { get; set; }
    
}