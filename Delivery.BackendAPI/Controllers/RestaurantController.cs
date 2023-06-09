using System.Runtime.InteropServices;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for interaction and management with restaurants
/// </summary>
[ApiController]
[Route("api/restaurant")]
public class RestaurantController : ControllerBase {
    private readonly IRestaurantService _restaurantService;
    private readonly IPermissionCheckerService _permissionCheckerService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="restaurantService"></param>
    /// <param name="permissionCheckerService"></param>
    public RestaurantController(IRestaurantService restaurantService,
        IPermissionCheckerService permissionCheckerService) {
        _restaurantService = restaurantService;
        _permissionCheckerService = permissionCheckerService;
    }

    /// <summary>
    /// [Anyone] Get list of available restaurants (non-archived restaurants)
    /// </summary>
    /// <param name="name">Name of restaurant for search</param>
    /// <param name="sort"></param>
    /// <param name="pageSize"></param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Pagination<RestaurantShortDto>>> GetRestaurants([FromQuery] String? name = null,
        [FromQuery] RestaurantSort sort = RestaurantSort.NameAsc, [FromQuery] int pageSize = 10,
        [FromQuery] int page = 1) {
        return Ok(await _restaurantService.GetAllUnarchivedRestaurants(page, pageSize, sort, name));
    }

    /// <summary>
    /// [Anyone] Get information about restaurant.
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}")]
    public async Task<ActionResult<RestaurantFullDto>> GetRestaurant([FromRoute] Guid restaurantId) {
        return Ok(await _restaurantService.GetRestaurant(restaurantId));
    }

    /// <summary>
    /// [Manager] Change information about restaurant.  
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantEditDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{restaurantId}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
    public async Task<ActionResult> EditRestaurant([FromRoute] Guid restaurantId, RestaurantEditDto restaurantEditDto) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        await _permissionCheckerService.IsUserManagerOfRestaurant(userId, restaurantId);
        await _restaurantService.EditRestaurant(restaurantId, restaurantEditDto);
        return Ok();
    }

    /// <summary>
    /// [Manager] Get orders from specific restaurant. 
    /// </summary>
    /// <remarks>
    /// Manager see all the orders in restaurant.<br/>
    /// </remarks>
    /// <param name="restaurantId"></param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number">Order number for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}/orders")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Manager")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetRestaurantOrders([FromRoute] Guid restaurantId,
        [FromQuery] [Optional] List<OrderStatus>? status, [FromQuery] [Optional] String? number,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 1, [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        if (await _permissionCheckerService.IsUserManagerOfRestaurant(userId, restaurantId) == false) {
            throw new ForbiddenException("You are not manager of this restaurant");
        }
        return Ok(await _restaurantService.GetRestaurantOrders(restaurantId, sort, status, number, page, pageSize));
    }

    /// <summary>
    /// [Cook] Get cook orders from specific restaurant. 
    /// </summary>
    /// <remarks>
    /// Cook see orders in his restaurant with statuses: Created
    /// </remarks>
    /// <param name="restaurantId"></param>
    /// <param name="number">Order number for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}/orders/cook")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Cook")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCookRestaurantOrders([FromRoute] Guid restaurantId,
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] int pageSize = 1,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        if (await _permissionCheckerService.IsUserCookOfRestaurant(userId, restaurantId) == false) {
            throw new ForbiddenException("You are not cook of this restaurant");
        }
        return Ok(await _restaurantService.GetCookRestaurantOrders(restaurantId, sort, number, page, pageSize));
    }
}