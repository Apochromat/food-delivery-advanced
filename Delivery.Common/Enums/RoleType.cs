using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.Enums; 
/// <summary>
/// Role types
/// </summary>
public enum RoleType {
    /// <summary>
    /// Administrator role
    /// </summary>
    [Display(Name = ApplicationRoleNames.Administrator)]
    Administrator,
    /// <summary>
    /// Manager role
    /// </summary>
    [Display(Name = ApplicationRoleNames.Manager)]
    Manager,
    /// <summary>
    /// Cook role
    /// </summary>
    [Display(Name = ApplicationRoleNames.Cook)]
    Cook,
    /// <summary>
    /// Courier role
    /// </summary>
    [Display(Name = ApplicationRoleNames.Courier)]
    Courier,
    /// <summary>
    /// Customer role
    /// </summary>
    [Display(Name = ApplicationRoleNames.Customer)]
    Customer,
}

/// <summary>
/// Role names
/// </summary>
public class ApplicationRoleNames {
    /// <summary>
    /// Administrator role name
    /// </summary>
    public const string Administrator = "Administrator";
    /// <summary>
    /// Manager role name
    /// </summary>
    public const string Manager = "Manager";
    /// <summary>
    /// Cook role name
    /// </summary>
    public const string Cook = "Cook";
    /// <summary>
    /// Courier role name
    /// </summary>
    public const string Courier = "Courier";
    /// <summary>
    /// Customer role name
    /// </summary>
    public const string Customer = "Customer";
}