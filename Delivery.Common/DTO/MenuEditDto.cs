using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO;

/// <summary>
/// Menu DTO for editing
/// </summary>
public class MenuEditDto {
    /// <summary>
    /// Name of the menu
    /// </summary>
    [Required]
    public required string Name { get; set; }
}