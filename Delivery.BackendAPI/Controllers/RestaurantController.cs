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
    /// <param name="name" example="Claude Monet">Name of restaurant for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<Pagination<RestaurantShortDto>> GetRestaurants([FromQuery] String name, [FromQuery] int page = 1 ) {
        return Ok(_restaurantService.GetAllUnarchivedRestaurants(name, page));
    }

    /// <summary>
    /// [Anyone] Get information about restaurant, list of menus.
    /// If menu is archived, it is available only for Manager and Administrator.
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}")]
    public ActionResult<RestaurantFullDto> GetRestaurant([FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Manager] Change information about restaurant.  
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantUpdateDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{restaurantId}")]
    public ActionResult EditRestaurant([FromRoute] Guid restaurantId, RestaurantUpdateDto restaurantUpdateDto) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Manager] Get orders from specific restaurant. 
    /// </summary>
    /// <remarks>
    /// Manager see all the orders in restaurant.<br/>
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}/orders")]
    public ActionResult<Pagination<OrderShortDto>> GetRestaurantOrders([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Cook] Get cook orders from specific restaurant. 
    /// </summary>
    /// <remarks>
    /// Cook see orders in his restaurant with statuses: Created
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}/orders/cook")]
    public ActionResult<Pagination<OrderShortDto>> GetCookRestaurantOrders([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}