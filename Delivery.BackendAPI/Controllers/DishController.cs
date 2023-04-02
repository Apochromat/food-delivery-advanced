using System.Net;
using System.Runtime.InteropServices;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for dishes management
/// </summary>
[ApiController]
[Route("api/restaurant/{restaurantId}/dish")]
public class DishController : ControllerBase {
    /// <summary>
    /// [Manager] Creates a new dish
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CreateRestaurantDish([FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Anyone] Get list of dishes in restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="menus">List of menus to search.<br/>
    /// If empty, then the search goes through all the dishes of the restaurant.
    /// If multiple menus are selected, union of them is used</param>
    /// <param name="categories">List of categories to search.
    /// If empty, then the search goes through all the dishes categories of the restaurant.
    /// If multiple categories are selected, union of them is used</param>
    /// <param name="sort">Sorting type</param>
    /// <param name="name">Dish name for search</param>
    /// <param name="isVegetarian">Should a list of only veggie dishes be returned</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<Pagination<DishShortDto>> GetDishes([FromRoute] Guid restaurantId, [FromQuery] [Optional] List<Guid>? menus,
        [FromQuery] [Optional] List<DishCategory>? categories, [FromQuery] [Optional] String? name, 
        [FromQuery] Boolean isVegetarian = false, [FromQuery] int page = 1, [FromQuery] DishSort? sort = DishSort.NameAsc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Anyone] Get information about dish. If dish is archived, it is available only for Manager and Administrator.
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{dishId}")]
    public ActionResult<DishFullDto> GetDish([FromRoute] Guid dishId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Manager] Edit information about dish
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="dishEditDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{dishId}/edit")]
    public ActionResult EditDish([FromRoute] Guid dishId, [FromBody] DishEditDto dishEditDto) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Manager] Archive dish
    /// </summary>
    /// <remarks>
    /// Set dish archived. It becomes available only for Manager and Administrator
    /// </remarks>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{dishId}/archive")]
    public ActionResult ArchiveDish([FromRoute] Guid dishId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Manager] Get list of archived dishes
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("archived")]
    public ActionResult ArchivedDishes([FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Manager] Unarchive dish
    /// </summary>
    /// <remarks>
    /// Set dish unarchived. It becomes available for all users
    /// </remarks>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{dishId}/unarchive")]
    public ActionResult UnarchiveDish([FromRoute] Guid dishId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Customer] Provides information about the possibility of setting a rating for a dish by the user 
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{dishId}/rating/check")]
    public ActionResult<RatingCheckDto> RatingCheck([FromRoute] Guid dishId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Customer] Set user rating for the dish 
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="ratingSetDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{dishId}/rating")]
    public ActionResult Rating([FromRoute] Guid dishId, [FromBody] RatingSetDto ratingSetDto) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}