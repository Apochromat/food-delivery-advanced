using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO; 

/// <summary>
/// DTO for creating new menu
/// </summary>
public class MenuCreateDto {
    /// <summary>
    /// Menu name
    /// </summary>
    [Required]
    public String Name { get; set; } = "";
}