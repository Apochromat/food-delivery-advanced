using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AuthAPI.Controllers;

/// <summary>
/// Controller for getting user`s account information
/// </summary>
[ApiController]
[Route("api")]
public class AccountController : ControllerBase {
    private readonly IAccountService _accountService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="accountService"></param>
    public AccountController(IAccountService accountService) {
        _accountService = accountService;
    }

    /// <summary>
    /// Get information about current authenticated user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("account")]
    public async Task<ActionResult<AccountProfileFullDto>> GetCurrentProfile() {
        if (User.Identity == null || User.Identity.Name == null) {
            throw new UnauthorizedException("Invalid authorisation");
        }

        return Ok(await _accountService.GetProfileAsync(User.Identity.Name));
    }

    /// <summary>   
    /// Edit current authenticated user`s profile
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("account")]
    public async Task<ActionResult> UpdateProfile([FromBody] AccountProfileEditDto accountProfileEditDto) {
        if (User.Identity == null || User.Identity.Name == null) {
            throw new UnauthorizedException("Invalid authorisation");
        }

        await _accountService.UpdateProfileAsync(User.Identity.Name, accountProfileEditDto);
        return Ok();
    }

    /// <summary>
    /// Get general information about courier
    /// </summary>
    /// <param name="courierId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("courier-profile/{courierId}")]
    public async Task<ActionResult<AccountCourierProfileDto>> GetCourierProfile([FromRoute] Guid courierId) {
        return Ok(await _accountService.GetCourierProfileAsync(courierId.ToString()));
    }

    /// <summary>
    /// Get general information about customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("customer-profile/{customerId}")]
    public async Task<ActionResult<AccountCustomerProfileDto>> GetCustomerProfile([FromRoute] Guid customerId) {
        return Ok(await _accountService.GetCustomerProfileAsync(customerId.ToString()));
    }

    /// <summary>
    /// Get general information about cook
    /// </summary>
    /// <param name="cookId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("cook-profile/{cookId}")]
    public async Task<ActionResult<AccountCustomerProfileDto>> GetCookProfile([FromRoute] Guid cookId) {
        return Ok(await _accountService.GetCookProfileAsync(cookId.ToString()));
    }
}