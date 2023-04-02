using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AuthAPI.Controllers; 

/// <summary>
/// Controller for register, authentication, changing the password
/// </summary>
[ApiController]
[Route("api")]
public class AuthController : ControllerBase {
    /// <summary>
    /// Register new user as Customer
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("register")]
    public ActionResult Register() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
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
    public ActionResult Login() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
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