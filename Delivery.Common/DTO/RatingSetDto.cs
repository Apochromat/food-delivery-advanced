using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO;

/// <summary>
/// DTO for rating set
/// </summary>
public class RatingSetDto {
    /// <summary>
    /// Rating value
    /// </summary>
    [Required]
    [Range(1, 5)]
    public required int Rating { get; set; }
}