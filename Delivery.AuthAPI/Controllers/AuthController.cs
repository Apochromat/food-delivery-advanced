﻿using System.Net;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AuthAPI.Controllers; 

/// <summary>
/// Controller for register, authentication, changing the password
/// </summary>
[ApiController]
[Route("api")]
public class AuthController : ControllerBase {
    private readonly IAuthService _authService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="authService"></param>
    public AuthController(IAuthService authService) {
        _authService = authService;
    }

    /// <summary>
    /// Register new user as Customer
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<TokenResponseDto>> Register([FromBody] AccountRegisterDto accountRegisterDto) {
        return Ok(await _authService.RegisterAsync(accountRegisterDto, HttpContext));
    }
    
    /// <summary>
    /// Login user into the system
    /// </summary>
    /// <remarks>
    /// Returns the user access-token(JWT) and refresh-token(non-JWT), a list of roles.<br/>
    /// If the user logs in from any device, this device is remembered to work with a specific refresh-token
    /// </remarks>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenResponseDto>> Login([FromBody] AccountLoginDto accountLoginDto) {
        return Ok(await _authService.LoginAsync(accountLoginDto, HttpContext));
    }
    
    /// <summary>
    /// Refreshes access-token
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult<TokenResponseDto>> Refresh([FromBody] TokenRequestDto tokenRequestDto) {
        return Ok(await _authService.RefreshTokenAsync(tokenRequestDto.AccessToken, tokenRequestDto.RefreshToken, HttpContext));
    }
    
    /// <summary>
    /// Changes user password
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto) {
        await _authService.ChangePasswordAsync(User.Identity.Name, changePasswordDto);
        return Ok();    
    }
    
    /// <summary>
    /// Get user devices
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("devices")]
    public async Task<ActionResult<List<DeviceDto>>> GetDevices() {
        return Ok(await _authService.GetDevicesAsync(User.Identity.Name));
    }
    
    /// <summary>
    /// Rename device
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="deviceRenameDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("devices/{deviceId}")]
    public async Task<ActionResult> RenameDevice([FromRoute] Guid deviceId, [FromBody] DeviceRenameDto deviceRenameDto) {
        await _authService.RenameDeviceAsync(User.Identity.Name, deviceId, deviceRenameDto);
        return Ok();
    }
    
    /// <summary>
    /// Delete device from user devices
    /// </summary>
    /// <param name="deviceId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("devices/{deviceId}")]
    public async Task<ActionResult> DeleteDevice([FromRoute] Guid deviceId) {
        await _authService.DeleteDeviceAsync(User.Identity.Name, deviceId);
        return Ok();
    }
}