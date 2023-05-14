using System.Security.Claims;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Delivery.AdminPanel.BL.Services;

/// <summary>
/// Service for working with user account in admin panel
/// </summary>
public class AdminPanelAccountService : IAdminPanelAccountService {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    public AdminPanelAccountService(UserManager<User> userManager, SignInManager<User> signInManager) {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task Login(LoginViewDto model) {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) {
            throw new BadRequestException("Invalid email or password");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) {
            throw new BadRequestException("Invalid email or password");
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