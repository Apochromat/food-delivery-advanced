﻿using Delivery.Common.DTO;
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
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        return Ok(await _accountService.GetProfileAsync(userId));
    }

    /// <summary>
    /// Get information about Customer of current authenticated user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Customer")]
    [Route("account/customer")]
    public async Task<ActionResult<AccountCustomerProfileFullDto>> GetCurrentCustomerProfile() {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        return Ok(await _accountService.GetCustomerFullProfileAsync(userId));
    }

    /// <summary>   
    /// Edit current authenticated user`s profile
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("account")]
    public async Task<ActionResult> UpdateProfile([FromBody] AccountProfileEditDto accountProfileEditDto) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }
        
        await _accountService.EditProfileAsync(userId, accountProfileEditDto);
        return Ok();
    }

    /// <summary>   
    /// Edit current authenticated user`s customer profile
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Customer")]
    [Route("account/customer")]
    public async Task<ActionResult> UpdateCustomerProfile(
        [FromBody] AccountCustomerProfileEditDto accountCustomerProfileEditDto) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        await _accountService.EditCustomerProfileAsync(userId, accountCustomerProfileEditDto);
        return Ok();
    }

    /// <summary>
    /// Get general information about courier
    /// </summary>
    /// <param name="courierId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("courier-profile/{courierId}")]
    public async Task<ActionResult<AccountCourierProfileDto>> GetCourierProfile([FromRoute] Guid courierId) {
        return Ok(await _accountService.GetCourierProfileAsync(courierId));
    }

    /// <summary>
    /// Get general information about customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("customer-profile/{customerId}")]
    public async Task<ActionResult<AccountCustomerProfileDto>> GetCustomerProfile([FromRoute] Guid customerId) {
        return Ok(await _accountService.GetCustomerProfileAsync(customerId));
    }

    /// <summary>
    /// Get general information about cook
    /// </summary>
    /// <param name="cookId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("cook-profile/{cookId}")]
    public async Task<ActionResult<AccountCustomerProfileDto>> GetCookProfile([FromRoute] Guid cookId) {
        return Ok(await _accountService.GetCookProfileAsync(cookId));
    }
}