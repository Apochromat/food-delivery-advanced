using System.ComponentModel;

namespace Delivery.Common.DTO;

/// <summary>
/// Token response DTO
/// </summary>
public class TokenResponseDto {
    /// <summary>
    /// Access token
    /// </summary>
    [DisplayName("access_token")]
    public required string AccessToken { get; set; }

    /// <summary>
    /// Refresh token
    /// </summary>
    [DisplayName("refresh_token")]
    public required string RefreshToken { get; set; }
}