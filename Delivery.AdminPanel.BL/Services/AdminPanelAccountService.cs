using System.Security.Claims;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Delivery.AdminPanel.BL.Services; 

/// <summary>
/// Service for working with user account in admin panel
/// </summary>
public class AdminPanelAccountService : IAdminPanelAccountService {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AdminPanelAccountService> _logger;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="logger"></param>>
    /// <param name="roleManager"></param>
    public AdminPanelAccountService(UserManager<User> userManager,
        SignInManager<User> signInManager, ILogger<AdminPanelAccountService> logger, RoleManager<IdentityRole<Guid>> roleManager) {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _roleManager = roleManager;
    }
    
    public async Task Login(LoginViewDto model) {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) {
            throw new KeyNotFoundException($"User with email {model.Email} does not found");
        }

        var claims = new List<Claim> {
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        if (!roles.Contains(RoleType.Administrator.ToString())) {
            throw new MethodNotAllowedException("Only admin can login to admin panel");
        }
        if (roles.Any()) {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }

        var authProperties = new AuthenticationProperties {
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(2),
            IsPersistent = true
        };
        
        await _signInManager.SignInWithClaimsAsync(user, authProperties, claims);
    }

    public async Task Logout() {
        await _signInManager.SignOutAsync();
    }
}