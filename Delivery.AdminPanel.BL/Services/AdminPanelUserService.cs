﻿using AutoMapper;
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
    private readonly BackendDbContext _backendDbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;

    public AdminPanelUserService(AuthDbContext authDbContext, UserManager<User> userManager, BackendDbContext backendDbContext, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper) {
        _authDbContext = authDbContext;
        _userManager = userManager;
        _backendDbContext = backendDbContext;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<Pagination<AccountProfileFullDto>> GetAllUsers(string? name, int page, int pageSize = 10) {
        var allCount = _backendDbContext.Restaurants
            .Count(x => name == null ? true : x.Name.Contains(name));
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
        }
        return new Pagination<AccountProfileFullDto>(mapped, page, pageSize, mapped.Count());
    }

    public async Task EditUser(Guid userId, AdminPanelAccountProfileEditDto model) {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        
        user.FullName = model.FullName;
        user.Gender = model.Gender ?? user.Gender;
        
        //await _authDbContext.SaveChangesAsync();
        
        var removeResult = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
        if (!removeResult.Succeeded) {
            throw new ArgumentException(string.Join(", ", removeResult.Errors.Select(x => x.Description)));
        }
        
        var addResult = await _userManager.AddToRolesAsync(user, model.Roles);
        if (!addResult.Succeeded) {
            throw new ArgumentException(string.Join(", ", addResult.Errors.Select(x => x.Description)));
        }
        var roled = await _userManager.GetRolesAsync(user);

        await _userManager.UpdateAsync(user);
    }
}