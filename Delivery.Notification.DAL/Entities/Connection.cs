using System.ComponentModel.DataAnnotations;

namespace Delivery.Notification.DAL.Entities; 

/// <summary>
/// Connection entity
/// </summary>
public class Connection {
    /// <summary>
    /// Entity Identifier
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// User identifier
    /// </summary>
    public Guid ReceiverId { get; set; }
    /// <summary>
    /// Connection Identifier
    /// </summary>
    public required string ConnectionId { get; set; }
    /// <summary>
    /// Connection creation date
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}