using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// Account profile DTO
/// </summary>
public class AccountCustomerProfileDto {
    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    public string? FullName { get; set; }
    /// <summary>
    /// User`s gender
    /// </summary>
    public Gender? Gender { get; set; }
}