namespace Delivery.Common.DTO; 

/// <summary>
/// LoginView DTO for LoginViewModel in AdminPanel
/// </summary>
public class LoginViewDto {
    /// <summary>
    /// User email
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// User password
    /// </summary>
    public string? Password { get; set; }
}