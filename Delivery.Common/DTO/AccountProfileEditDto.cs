using System.ComponentModel.DataAnnotations;
using Delivery.Common.Enums;

namespace Delivery.Common.DTO;

/// <summary>
/// Account profile DTO
/// </summary>
public class AccountProfileEditDto {
    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// User`s birth date
    /// </summary>
    [Required]
    [Range(typeof(DateTime), "01/01/1900", "01/01/2023")]
    public required DateTime BirthDate { get; set; }

    /// <summary>
    /// User`s gender
    /// </summary>
    [Required]
    public required Gender Gender { get; set; }
}