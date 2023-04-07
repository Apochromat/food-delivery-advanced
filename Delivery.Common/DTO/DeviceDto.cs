namespace Delivery.Common.DTO; 

/// <summary>
/// DTO for device
/// </summary>
public class DeviceDto {
    /// <summary>
    /// Device identifier
    /// </summary>
    public Guid Id { get; set; }
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
    /// Last activity
    /// </summary>
    public DateTime? LastActivity { get; set; }
}