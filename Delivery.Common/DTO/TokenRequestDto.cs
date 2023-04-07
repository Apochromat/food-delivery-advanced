using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Delivery.Common.Enums;

namespace Delivery.Common.DTO; 

/// <summary>
/// Token request DTO
/// </summary>
public class TokenRequestDto {
    /// <summary>
    /// Expired access token
    /// </summary>
    [Required]
    [DisplayName("access_token")]
    public String AccessToken { get; set; } = "";
    /// <summary>
    /// Refresh token
    /// </summary>
    [Required]
    [DisplayName("refresh_token")]
    public String RefreshToken { get; set; } = "";
}