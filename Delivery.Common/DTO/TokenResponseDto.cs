using System.ComponentModel;
using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// Token response DTO
/// </summary>
public class TokenResponseDto {
    /// <summary>
    /// Access token
    /// </summary>
    [DisplayName("access_token")]
    public String? AccessToken { get; set; }
    /// <summary>
    /// Refresh token
    /// </summary>
    [DisplayName("refresh_token")]
    public String? RefreshToken { get; set; }
}