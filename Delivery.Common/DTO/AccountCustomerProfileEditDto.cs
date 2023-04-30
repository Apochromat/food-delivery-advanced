using System.ComponentModel.DataAnnotations;

namespace Delivery.Common.DTO; 

/// <summary>
/// Account customer profile edit DTO
/// </summary>
public class AccountCustomerProfileEditDto {
    /// <summary>
    /// User`s address
    /// </summary>
    [Required]
    public String Address { get; set; } = "";
}