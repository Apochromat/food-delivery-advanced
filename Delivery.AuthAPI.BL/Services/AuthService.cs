using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    public AuthService(ILogger<AuthService> logger, UserManager<User> userManager, SignInManager<User> signInManager,
        AuthDbContext authDbContext) {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _authDbContext = authDbContext;
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
            throw new IncorrectLoginException("Incorrect username or password");
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
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<TokenResponseDto> RefreshTokenAsync(string refreshToken) {
        throw new NotImplementedException();
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