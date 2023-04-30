﻿using Delivery.Common.DTO;

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
    /// Get customer full profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<AccountCustomerProfileFullDto> GetCustomerFullProfileAsync(string userId);
    
    /// <summary>
    /// Edit current user profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountProfileEditDto"></param>
    /// <returns></returns>
    Task EditProfileAsync(string userId, AccountProfileEditDto accountProfileEditDto);
    
    /// <summary>
    /// Edit customer profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountCustomerProfileEditDto"></param>
    /// <returns></returns>
    Task EditCustomerProfileAsync(string userId, AccountCustomerProfileEditDto accountCustomerProfileEditDto);
    
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