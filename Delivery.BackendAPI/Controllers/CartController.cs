using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for Customer`s cart management
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/cart")]
public class CartController : ControllerBase {
    private readonly ICartService _cartService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="cartService"></param>
    public CartController(ICartService cartService) {
        _cartService = cartService;
    }

    /// <summary>
    /// [Customer] Get user`s cart
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Customer")]
    public async Task<ActionResult<CartDto>> GetCart() {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        return Ok(await _cartService.GetCart(userId));
    }

    /// <summary>
    /// [Customer] Add dish to user`s cart. If cart includes dish from another restaurant, adding is blocked
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("dish/{dishId}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Customer")]
    public async Task<ActionResult> AddDishToCart([FromRoute] Guid dishId, [FromQuery] int amount = 1) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        await _cartService.AddDishToCart(userId, dishId, amount);
        return Ok();
    }

    /// <summary>
    /// [Customer] Remove dish from user`s cart
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="removeAll">If True, then removes all instances of the dish from the basket,
    ///     else removes only one (or amount) of them</param>
    /// <param name="amount"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("dish/{dishId}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Customer")]
    public async Task<ActionResult> RemoveDishFromCart([FromRoute] Guid dishId, [FromQuery] Boolean removeAll = false,
        [FromQuery] int amount = 1) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        await _cartService.RemoveDishFromCart(userId, dishId, removeAll, amount);
        return Ok();
    }

    /// <summary>
    /// [Customer] Clear user`s cart
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Customer")]
    public async Task<ActionResult> ClearCart() {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        await _cartService.ClearCart(userId);
        return Ok();
    }
}