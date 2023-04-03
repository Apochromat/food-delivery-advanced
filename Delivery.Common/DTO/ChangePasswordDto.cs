using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO; 

public class ChangePasswordDto {
    /// <summary>
    /// Old password
    /// </summary>
    [Required]
    [DisplayName("old_password")]
    public String OldPassword { get; set; } = "";
    /// <summary>
    /// New password
    /// </summary>
    [Required]
    [DisplayName("new_password")]
    public String NewPassword { get; set; } = "";
}