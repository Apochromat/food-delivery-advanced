using AutoMapper;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Delivery.AuthAPI.BL.Services;

/// <summary>
/// Service for account info management
/// </summary>
public class AccountService : IAccountService {
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="userManager"></param>
    public AccountService(IMapper mapper, UserManager<User> userManager) {
        _mapper = mapper;
        _userManager = userManager;
    }

    /// <summary>
    /// Get full current user profile
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<AccountProfileFullDto> GetProfileAsync(string userId) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        var profile = _mapper.Map<AccountProfileFullDto>(user);
        profile.Roles = (await _userManager.GetRolesAsync(user)).ToList();
        return profile;
    }

    /// <summary>
    /// Return full information about customer
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<AccountCustomerProfileFullDto> GetCustomerFullProfileAsync(string userId) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.Users.Include(u => u.Customer)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        var profile = _mapper.Map<AccountCustomerProfileFullDto>(user.Customer);
        return profile;
    }

    /// <summary>
    /// Update current user profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountProfileEditDto"></param>
    /// <returns></returns>
    public async Task EditProfileAsync(string userId, AccountProfileEditDto accountProfileEditDto) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        user.FullName = accountProfileEditDto.FullName;
        user.BirthDate = accountProfileEditDto.BirthDate;
        user.Gender = accountProfileEditDto.Gender;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) {
            throw new InvalidOperationException("User update failed");
        }
    }

    /// <summary>
    /// Update current user customer profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountCustomerProfileEditDto"></param>
    public async Task EditCustomerProfileAsync(string userId,
        AccountCustomerProfileEditDto accountCustomerProfileEditDto) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.Users.Include(u => u.Customer)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (user.Customer == null) {
            throw new NotFoundException("Customer not found");
        }

        user.Customer.Address = accountCustomerProfileEditDto.Address;
        await _userManager.UpdateAsync(user);
    }

    /// <summary>
    /// Get short information about courier
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<AccountCourierProfileDto> GetCourierProfileAsync(string userId) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.Users.Include(u => u.Courier).FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (!await _userManager.IsInRoleAsync(user, RoleType.Courier.ToString())) {
            throw new NotFoundException("Courier not found");
        }

        var profile = _mapper.Map<AccountCourierProfileDto>(user);
        return profile;
    }

    /// <summary>
    /// Get short information about cook
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<AccountCookProfileDto> GetCookProfileAsync(string userId) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.Users.Include(u => u.Cook).FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (!await _userManager.IsInRoleAsync(user, RoleType.Cook.ToString())) {
            throw new NotFoundException("Cook not found");
        }

        var profile = _mapper.Map<AccountCookProfileDto>(user);
        return profile;
    }

    /// <summary>
    /// Get short information about customer
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<AccountCustomerProfileDto> GetCustomerProfileAsync(string userId) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.Users.Include(u => u.Customer)
            .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (!await _userManager.IsInRoleAsync(user, RoleType.Customer.ToString())) {
            throw new NotFoundException("Customer not found");
        }

        var profile = _mapper.Map<AccountCustomerProfileDto>(user);
        return profile;
    }
}