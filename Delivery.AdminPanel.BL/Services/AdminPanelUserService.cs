using AutoMapper;
using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.BackendAPI.DAL;
using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Delivery.AdminPanel.BL.Services; 

public class AdminPanelUserService : IAdminPanelUserService {
    private readonly AuthDbContext _authDbContext;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AdminPanelUserService(AuthDbContext authDbContext, UserManager<User> userManager, IMapper mapper) {
        _authDbContext = authDbContext;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Pagination<AccountProfileFullDto>> GetAllUsers(string? name, int page, int pageSize = 10) {
        var allCount = _authDbContext.Users
            .Count(x => name == null ? true : x.FullName.Contains(name));
        if (allCount == 0) {
            return new Pagination<AccountProfileFullDto>(new List<AccountProfileFullDto>(), page, pageSize, 0);
        }

        // Calculate pages amount
        var pages = (int)Math.Ceiling((double)allCount / pageSize);
        if (page > pages) {
            throw new NotFoundException("Users not found");
        }

        // Get users
        var raw = _authDbContext.Users?
            .Where(x => name == null ? true : x.FullName.Contains(name))
            .OrderBy(x => x.FullName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var mapped = _mapper.Map<List<AccountProfileFullDto>>(raw);
        foreach (var user in mapped) {
            var dbUser = await _userManager.FindByIdAsync(user.Id.ToString());
            var roles = await _userManager.GetRolesAsync(dbUser);
            user.Roles = roles.ToList();
            user.IsBanned = await IsUserBanned(user.Id);
        }
        return new Pagination<AccountProfileFullDto>(mapped, page, pageSize, pages);
    }

    public async Task EditUser(Guid userId, AdminPanelAccountProfileEditDto model) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        
        user.FullName = model.FullName;
        user.Gender = model.Gender ?? user.Gender;
        
        var removeResult = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
        if (!removeResult.Succeeded) {
            throw new ArgumentException(string.Join(", ", removeResult.Errors.Select(x => x.Description)));
        }
        
        var addResult = await _userManager.AddToRolesAsync(user, model.Roles);
        if (!addResult.Succeeded) {
            throw new ArgumentException(string.Join(", ", addResult.Errors.Select(x => x.Description)));
        }

        await _userManager.UpdateAsync(user);
    }

    public async Task<bool> IsUserBanned(Guid userId) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        return await _userManager.IsLockedOutAsync(user);
    }

    public async Task BanUser(Guid userId) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(100).ToUniversalTime());
    }

    public async Task UnbanUser(Guid userId) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        await _userManager.SetLockoutEndDateAsync(user, null);
    }
}