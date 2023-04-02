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
    /// [AdminPanel] Creates a new restaurant, his Default Menu.
    /// </summary>
    /// <param name="restaurantCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CreateRestaurant([FromBody] RestaurantCreateDto restaurantCreateDto) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Anyone] Get list of available restaurants (non-archived restaurants)
    /// </summary>
    /// <param name="name" example="Claude Monet">Name of restaurant for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<Pagination<RestaurantShortDto>> GetRestaurants([FromQuery] String name, [FromQuery] int page = 1 ) {
        return Ok(_restaurantService.GetAllRestaurants(name, page));
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
    /// [AdminPanel, Manager] Change information about restaurant.  
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
    /// [AdminPanel] Archive restaurant.
    /// </summary>
    /// <remarks>
    /// Set restaurant archived. It becomes available only for Manager and Administrator
    /// </remarks>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{restaurantId}/archive")]
    public ActionResult ArchiveRestaurant([FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [AdminPanel] Get list of archived restaurants.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("archived")]
    public ActionResult ArchivedRestaurants() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [AdminPanel] Unarchive restaurant.
    /// </summary>
    /// <remarks>
    /// Set restaurant unarchived. It becomes available for all users.
    /// </remarks>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{restaurantId}/unarchive")]
    public ActionResult UnarchiveRestaurant([FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [AdminPanel, Manager, Cook] Get orders from specific restaurant. 
    /// </summary>
    /// <remarks>
    /// Administrator see all the orders in restaurant.<br/>
    /// Manager see all the orders in restaurant.<br/>
    /// Cook see orders in his restaurant with statuses: Created, Kitchen, Packaged, Delivered(history of cooked orders).<br/>
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{restaurantId}/order")]
    public ActionResult<Pagination<OrderShortDto>> GetOrders([FromQuery] [Optional] OrderSort? sort,
        [FromQuery] [Optional] List<OrderStatus>? status, [FromQuery] [Optional] String? number, [FromQuery] int page = 1 ) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}