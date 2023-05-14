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
    /// Logout user by deleting his current device
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    Task LogoutAsync(string userId, HttpContext httpContext);

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
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<DeviceDto>> GetDevicesAsync(string userId);

    /// <summary>
    /// Rename device
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deviceId"></param>
    /// <param name="deviceRenameDto"></param>
    /// <returns></returns>
    Task RenameDeviceAsync(string userId, Guid deviceId, DeviceRenameDto deviceRenameDto);

    /// <summary>
    /// Delete device from user devices
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deviceId"></param>
    /// <returns></returns>
    Task DeleteDeviceAsync(string userId, Guid deviceId);

    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="changePasswordDto"></param>
    /// <returns></returns>
    Task ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
}