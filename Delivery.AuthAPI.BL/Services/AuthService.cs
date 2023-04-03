﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.Configurations;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Delivery.AuthAPI.BL.Services;

/// <summary>
/// Service for authentication and authorization
/// </summary>
public class AuthService : IAuthService {
    private readonly ILogger<AuthService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AuthDbContext _authDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="userManager"></param>
    /// <param name="signInManager"></param>
    /// <param name="authDbContext"></param>
    /// <param name="mapper"></param>
    public AuthService(ILogger<AuthService> logger, UserManager<User> userManager, SignInManager<User> signInManager,
        AuthDbContext authDbContext, IMapper mapper) {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _authDbContext = authDbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="accountRegisterDto"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TokenResponseDto> RegisterAsync(AccountRegisterDto accountRegisterDto, HttpContext httpContext) {
        if (accountRegisterDto.Email == null) {
            throw new ArgumentNullException(nameof(accountRegisterDto.Email));
        }

        if (accountRegisterDto.Password == null) {
            throw new ArgumentNullException(nameof(accountRegisterDto.Password));
        }

        if (await _userManager.FindByEmailAsync(accountRegisterDto.Email) != null) {
            throw new ConflictException("User with this email already exists");
        }

        var user = new Customer() {
            UserName = accountRegisterDto.Email,
            Email = accountRegisterDto.Email,
            FullName = accountRegisterDto.FullName,
            Address = "",
            Gender = accountRegisterDto.Gender,
            JoinedAt = DateTime.Now.ToUniversalTime(),
            BirthDate = accountRegisterDto.BirthDate
        };

        var result = await _userManager.CreateAsync(user, accountRegisterDto.Password);

        if (result.Succeeded) {
            _logger.LogInformation("Successful register");
            await _userManager.AddToRoleAsync(user, ApplicationRoleNames.Customer);

            return await LoginAsync(new AccountLoginDto()
                { Email = accountRegisterDto.Email, Password = accountRegisterDto.Password }, httpContext);
        }

        var errors = string.Join(", ", result.Errors.Select(x => x.Description));
        throw new InvalidOperationException(errors);
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="accountLoginDto"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TokenResponseDto> LoginAsync(AccountLoginDto accountLoginDto, HttpContext httpContext) {
        if (accountLoginDto.Email == null) {
            throw new ArgumentNullException(nameof(accountLoginDto.Email));
        }

        if (accountLoginDto.Password == null) {
            throw new ArgumentNullException(nameof(accountLoginDto.Password));
        }
        
        var identity = await GetIdentity(accountLoginDto.Email.ToLower(), accountLoginDto.Password);
        if (identity == null) {
            throw new BadRequestException("Incorrect username or password");
        }
        
        var user = _userManager.Users.Include(x => x.Devices).First(x => x.Email == accountLoginDto.Email);
        var device = user.Devices.FirstOrDefault(x => x.IpAddress == httpContext.Connection.RemoteIpAddress?.ToString());
        
        if (device == null) {
            device = new Device() {
                IpAddress = httpContext.Connection.RemoteIpAddress?.ToString(),
                User = user,
                RefreshToken = $"{Guid.NewGuid()}-{Guid.NewGuid()}",
                UserAgent = httpContext.Request.Headers["User-Agent"],
                CreatedAt = DateTime.Now.ToUniversalTime(),
                LastActivity = DateTime.Now.ToUniversalTime(),
                ExpirationDate = DateTime.Now.AddMonths(6).ToUniversalTime()
            };
            await _authDbContext.Devices.AddAsync(device);
            await _authDbContext.SaveChangesAsync();
        }
        
        device.LastActivity = DateTime.Now.ToUniversalTime();
        await _authDbContext.SaveChangesAsync();
        
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: JwtConfiguration.Issuer,
            audience: JwtConfiguration.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(JwtConfiguration.Lifetime)),
            signingCredentials: new SigningCredentials(JwtConfiguration.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        _logger.LogInformation("Successful login");

        return new TokenResponseDto() {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
            RefreshToken = device.RefreshToken
        };
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <param name="jwtToken"></param>
    /// <param name="refreshToken"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TokenResponseDto> RefreshTokenAsync(string jwtToken, string refreshToken, HttpContext httpContext) {
        jwtToken = jwtToken.Replace("Bearer ", "");
        var principal = GetPrincipalFromExpiredToken(jwtToken);
        var jwt = new JwtSecurityToken(
            issuer: JwtConfiguration.Issuer,
            audience: null,
            notBefore: DateTime.UtcNow,
            claims: principal.Claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(JwtConfiguration.Lifetime)),
            signingCredentials: new SigningCredentials(JwtConfiguration.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        
        if (principal == null || principal.Identity == null) {
            throw new BadRequestException("Invalid jwt token");
        }
        
        var user = _userManager.Users.Include(x => x.Devices).First(x => x.Email == principal.Identity.Name);
        var device = user.Devices.FirstOrDefault(x => x.IpAddress == httpContext.Connection.RemoteIpAddress?.ToString());
        
        if (device == null) {
            throw new ForbiddenException("You can't refresh token from another device");
        }
        
        device.LastActivity = DateTime.Now.ToUniversalTime();
        await _authDbContext.SaveChangesAsync();
        
        return new TokenResponseDto() {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
            RefreshToken = device.RefreshToken
        };
    }
    
    /// <summary>
    /// Get user devices
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public Task<List<DeviceDto>> GetDevicesAsync(string email) {
        if (email == null) {
            throw new ArgumentNullException(nameof(email));
        }
        var user = _userManager.Users.Include(x=> x.Devices).First(u => u.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        var devices = _authDbContext.Devices.Where(d => d.User == user).ToList();
        return Task.FromResult(devices.Select(d => _mapper.Map<DeviceDto>(d)).ToList());
    }

    /// <summary>
    /// Rename device
    /// </summary>
    /// <param name="email"></param>
    /// <param name="deviceId"></param>
    /// <param name="deviceRenameDto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RenameDeviceAsync(string email, Guid deviceId, DeviceRenameDto deviceRenameDto) {
        if (email == null) {
            throw new ArgumentNullException(nameof(email));
        }
        var user = _userManager.Users.First(u => u.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        
        var device = _authDbContext.Devices.FirstOrDefault(d => d.User == user);
        if (device == null) {
            throw new NotFoundException("Device not found");
        }

        device.DeviceName = deviceRenameDto.DeviceName;
        await _authDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Delete device
    /// </summary>
    /// <param name="email"></param>
    /// <param name="deviceId"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task DeleteDeviceAsync(string email, Guid deviceId) {
        if (email == null) {
            throw new ArgumentNullException(nameof(email));
        }
        var user = _userManager.Users.First(u => u.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        var device = _authDbContext.Devices.FirstOrDefault(d => d.User == user);
        if (device == null) {
            throw new NotFoundException("Device not found");
        }

        _authDbContext.Devices.Remove(device);
        await _authDbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="email"></param>
    /// <param name="changePasswordDto"></param>
    public async Task ChangePasswordAsync(string email, ChangePasswordDto changePasswordDto) {
        if (email == null) {
            throw new ArgumentNullException(nameof(email));
        }
        var user = _userManager.Users.First(u => u.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string jwtToken) {
        var key = JwtConfiguration.GetSymmetricSecurityKey();

        var validationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = JwtConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtConfiguration.Audience,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    private async Task<ClaimsIdentity?> GetIdentity(string email, string password) {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) {
            return null;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) return null;

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Email ?? "")
        };

        foreach (var role in await _userManager.GetRolesAsync(user)) {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return new ClaimsIdentity(claims, "Token", ClaimTypes.Name, ClaimTypes.Role);
    }
}