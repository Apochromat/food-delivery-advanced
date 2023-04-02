using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AuthAPI.Controllers;

/// <summary>
/// Controller for getting user`s account information
/// </summary>
[ApiController]
[Route("api")]
public class AccountController : ControllerBase {
    /// <summary>
    /// Get information about current authenticated user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("account")]
    public ActionResult GetCurrentProfile() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// Edit current authenticated user`s profile
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Route("account")]
    public ActionResult UpdateProfile() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// Get general information about courier
    /// </summary>
    /// <param name="courierId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("courier-profile/{courierId}")]
    public ActionResult GetCourierProfile([FromRoute] Guid courierId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// Get general information about customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("customer-profile/{customerId}")]
    public ActionResult GetCustomerProfile([FromRoute] Guid customerId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}