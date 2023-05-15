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
    Task<AccountProfileFullDto> GetProfileAsync(Guid userId);

    /// <summary>
    /// Get customer full profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCustomerProfileFullDto> GetCustomerFullProfileAsync(Guid userId);

    /// <summary>
    /// Edit current user profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountProfileEditDto"></param>
    /// <returns></returns>
    Task EditProfileAsync(Guid userId, AccountProfileEditDto accountProfileEditDto);

    /// <summary>
    /// Edit customer profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountCustomerProfileEditDto"></param>
    /// <returns></returns>
    Task EditCustomerProfileAsync(Guid userId, AccountCustomerProfileEditDto accountCustomerProfileEditDto);

    /// <summary>
    /// Get courier short profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCourierProfileDto> GetCourierProfileAsync(Guid userId);

    /// <summary>
    /// Get cook short profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCookProfileDto> GetCookProfileAsync(Guid userId);

    /// <summary>
    /// Get customer short profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCustomerProfileDto> GetCustomerProfileAsync(Guid userId);
}