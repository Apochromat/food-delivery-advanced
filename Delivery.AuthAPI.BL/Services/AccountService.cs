using AutoMapper;
using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Delivery.AuthAPI.BL.Services;

/// <summary>
/// Service for account info management
/// </summary>
public class AccountService : IAccountService {
    private readonly ILogger<AccountService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AuthDbContext _authDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="authDbContext"></param>
    public AccountService(ILogger<AccountService> logger, IMapper mapper, UserManager<User> userManager,
        SignInManager<User> signInManager, AuthDbContext authDbContext) {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _authDbContext = authDbContext;
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
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    public async Task<AccountCustomerProfileFullDto> GetCustomerFullProfileAsync(string userId) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.Users.Include(u => u.Customer).FirstOrDefaultAsync(u => u.Id.ToString() == userId);
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
    public async Task UpdateProfileAsync(string userId, AccountProfileEditDto accountProfileEditDto) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        user.FullName = accountProfileEditDto.FullName ?? user.FullName;
        user.BirthDate = accountProfileEditDto.BirthDate ?? user.BirthDate;
        user.Gender = accountProfileEditDto.Gender ?? user.Gender;

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
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    public async Task UpdateCustomerProfileAsync(string userId, AccountCustomerProfileEditDto accountCustomerProfileEditDto) {
        if (userId == null) {
            throw new ArgumentException("User id is empty");
        }

        var user = await _userManager.Users.Include(u => u.Customer).FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        
        user.Customer.Address = accountCustomerProfileEditDto.Address ?? user.Customer.Address;
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

        if (user.Courier == null) {
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

        if (user.Cook == null) {
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

        var user = await _userManager.Users.Include(u => u.Customer).FirstOrDefaultAsync(u => u.Id.ToString() == userId);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (user.Customer == null) {
            throw new NotFoundException("Customer not found");
        }
        
        var profile = _mapper.Map<AccountCustomerProfileDto>(user);
        return profile;
    }
}