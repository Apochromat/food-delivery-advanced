using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Service for account management
/// </summary>
public interface IAccountService {
    /// <summary>
    /// Get current user profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountProfileFullDto> GetProfileAsync(string userId);
    
    /// <summary>
    /// Edit current user profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountProfileEditDto"></param>
    /// <returns></returns>
    Task UpdateProfileAsync(string userId, AccountProfileEditDto accountProfileEditDto);
    
    /// <summary>
    /// Get courier short profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCourierProfileDto> GetCourierProfileAsync(string userId);
    
    /// <summary>
    /// Get cook short profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCookProfileDto> GetCookProfileAsync(string userId);
    
    /// <summary>
    /// Get customer short profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCustomerProfileDto> GetCustomerProfileAsync(string userId);
}