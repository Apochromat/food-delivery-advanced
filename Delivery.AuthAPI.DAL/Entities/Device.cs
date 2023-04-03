using System.ComponentModel.DataAnnotations;

namespace Delivery.AuthAPI.DAL.Entities; 

/// <summary>
/// Entity for user devices, refresh tokens, etc.
/// </summary>
public class Device {
    /// <summary>
    /// Device identifier
    /// </summary>
    [Key]
    public Guid DeviceId { get; set; } = Guid.NewGuid();
    /// <summary>
    /// User identifier
    /// </summary>
    public User User { get; set; }
    /// <summary>
    /// Ip address
    /// </summary>
    public String? IpAddress { get; set; }
    /// <summary>
    /// User agent
    /// </summary>
    public String? UserAgent { get; set; }
    /// <summary>
    /// Device name
    /// </summary>
    public String? DeviceName { get; set; }
    /// <summary>
    /// Refresh token
    /// </summary>
    public String? RefreshToken { get; set; }
    /// <summary>
    /// Last activity
    /// </summary>
    public DateTime? LastActivity { get; set; }
    /// <summary>
    /// Date of creation
    /// </summary>
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime();
    /// <summary>
    /// Expiration date
    /// </summary>
    public DateTime ExpirationDate { get; set; }
}