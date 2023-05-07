using System.Net;
using System.Runtime.InteropServices;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for interaction and management with restaurants
/// </summary>
[ApiController]
[Route("api/restaurant")]
public class RestaurantController : ControllerBase {
    private readonly IRestaurantService _restaurantService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="restaurantService"></param>
    public RestaurantController(IRestaurantService restaurantService) {
        _restaurantService = restaurantService;
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
    public async Task<ActionResult<Pagination<RestaurantShortDto>>> GetRestaurants([FromQuery] String? name = null, [FromQuery] RestaurantSort sort = RestaurantSort.NameAsc, [FromQuery] int pageSize = 10, [FromQuery] int page = 1 ) {
        return Ok(await _restaurantService.GetAllUnarchivedRestaurants(page, pageSize, sort, name));
    }

    /// <summary>
    /// [Anyone] Get information about restaurant, list of menus.
    /// If menu is archived, it is available only for Manager and Administrator.
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
    public async Task<ActionResult> EditRestaurant([FromRoute] Guid restaurantId, RestaurantEditDto restaurantEditDto) {
        await _restaurantService.EditRestaurant(restaurantId, restaurantEditDto);
        // Todo: Check if user is manager
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
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}/orders")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetRestaurantOrders([FromRoute] Guid restaurantId, 
        [FromQuery] [Optional] List<OrderStatus>? status, [FromQuery] [Optional] String? number, 
        [FromQuery] int page = 1, [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        // Todo: Check if user is manager
        return Ok(await _restaurantService.GetRestaurantOrders(restaurantId, sort, status, number, page));
    }

    /// <summary>
    /// [Cook] Get cook orders from specific restaurant. 
    /// </summary>
    /// <remarks>
    /// Cook see orders in his restaurant with statuses: Created
    /// </remarks>
    /// <param name="restaurantId"></param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}/orders/cook")]
    public ActionResult<Pagination<OrderShortDto>> GetCookRestaurantOrders([FromRoute] Guid restaurantId, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        // Todo: Check if user is cook
        return Ok(_restaurantService.GetCookRestaurantOrders(restaurantId, sort, number, page));
    }
}