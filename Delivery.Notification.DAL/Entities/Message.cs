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
    public required string Title { get; set; }

    /// <summary>
    /// Message text
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// Message creation date
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Message delivery date
    /// </summary>
    public DateTime? DeliveredAt { get; set; }
}