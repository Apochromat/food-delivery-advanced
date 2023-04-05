using Delivery.Common.DTO;
using Microsoft.AspNetCore.Http;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Service for authentication and authorization
/// </summary>
public interface IAuthService {
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="accountRegisterDto"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task<TokenResponseDto> RegisterAsync(AccountRegisterDto accountRegisterDto, HttpContext httpContext);

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="accountLoginDto"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task<TokenResponseDto> LoginAsync(AccountLoginDto accountLoginDto, HttpContext httpContext);

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <param name="tokenRequestDto"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task<TokenResponseDto> RefreshTokenAsync(TokenRequestDto tokenRequestDto, HttpContext httpContext);
    
    /// <summary>
    /// Get user devices
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<List<DeviceDto>> GetDevicesAsync(string email);

    /// <summary>
    /// Rename device
    /// </summary>
    /// <param name="email"></param>
    /// <param name="deviceId"></param>
    /// <param name="deviceRenameDto"></param>
    /// <returns></returns>
    Task RenameDeviceAsync(string email, Guid deviceId, DeviceRenameDto deviceRenameDto);
    
    /// <summary>
    /// Delete device from user devices
    /// </summary>
    /// <param name="email"></param>
    /// <param name="deviceId"></param>
    /// <returns></returns>
    Task DeleteDeviceAsync(string email, Guid deviceId);

    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="email"></param>
    /// <param name="changePasswordDto"></param>
    /// <returns></returns>
    Task ChangePasswordAsync(string email, ChangePasswordDto changePasswordDto);
}