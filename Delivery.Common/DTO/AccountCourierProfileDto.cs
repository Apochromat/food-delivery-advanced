using Delivery.Common.Enums;

namespace Delivery.Common.DTO;

/// <summary>
/// Account profile DTO
/// </summary>
public class AccountCourierProfileDto {
    /// <summary>
    /// Profile Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// User`s gender
    /// </summary>
    public Gender? Gender { get; set; }
}