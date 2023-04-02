namespace Delivery.Common.DTO; 

/// <summary>
/// DTO for short info about menu
/// </summary>
public class MenuShortDto {
    /// <summary>
    /// Menu id
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Menu name
    /// </summary>
    public String? Name { get; set; }
}