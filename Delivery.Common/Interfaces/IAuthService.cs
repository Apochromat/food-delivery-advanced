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
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
}