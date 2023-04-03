using System.Net;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
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
    public ActionResult Refresh() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// Changes user password
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("change-password")]
    public ActionResult ChangePassword() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}