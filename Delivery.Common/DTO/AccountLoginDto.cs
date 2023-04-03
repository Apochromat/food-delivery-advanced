using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO; 

/// <summary>
/// Dto for login
/// </summary>
public class AccountLoginDto {
    /// <summary>
    /// User email
    /// </summary>
    [Required]
    [EmailAddress]
    [DisplayName("email")]
    public String? Email { get; set; }
    /// <summary>
    /// User password
    /// </summary>
    [Required]
    [DefaultValue("P@ssw0rd")]
    [DisplayName("password")]
    public String? Password { get; set; }
}