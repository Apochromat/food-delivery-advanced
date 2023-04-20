namespace Delivery.Common.DTO; 

/// <summary>
/// Account customer profile DTO
/// </summary>
public class AccountCustomerProfileFullDto {
    /// <summary>
    /// Profile Identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// User`s address
    /// </summary>
    public String? Address { get; set; }
}