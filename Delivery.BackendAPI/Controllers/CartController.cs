using System.Net;
using Delivery.Common.DTO;
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
    /// <summary>
    /// [Customer] Get user`s cart
    /// </summary>
    /// <returns></returns>
    [HttpGet] 
    public ActionResult<CartDto> GetCart() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Customer] Add dish to user`s cart. If cart includes dish from another restaurant, adding is blocked
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("dish/{dishId}")]
    public ActionResult AddDishToCart([FromRoute] Guid dishId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Customer] Remove dish from user`s cart
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="removeAll">If True, then removes all instances of the dish from the basket,
    /// else removes only one of them</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("dish/{dishId}")]
    public ActionResult RemoveDishFromCart([FromRoute] Guid dishId, [FromQuery] Boolean removeAll = false) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Customer] Clear user`s cart
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public ActionResult ClearCart() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}