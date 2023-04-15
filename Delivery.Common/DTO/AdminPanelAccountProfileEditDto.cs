using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// Data transfer object for admin panel user profile edit
/// </summary>
public class AdminPanelAccountProfileEditDto {
    /// <summary>
    /// User`s full name (surname, name, patronymic)
    /// </summary>
    public string? FullName { get; set; }
    /// <summary>
    /// User`s gender
    /// </summary>
    public Gender? Gender { get; set; }
    /// <summary>
    /// User roles
    /// </summary>
    public List<String> Roles { get; set; } = new List<String>();
}