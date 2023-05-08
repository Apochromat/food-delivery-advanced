using System.Net;
using System.Runtime.InteropServices;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for dishes management
/// </summary>
[ApiController]
[Route("api/restaurant/{restaurantId}/dish")]
public class DishController : ControllerBase {
    private readonly IDishService _dishService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dishService"></param>
    public DishController(IDishService dishService) {
        _dishService = dishService;
    }

    /// <summary>
    /// [Manager] Creates a new dish in restaurant default menu
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="dishCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateRestaurantDish([FromRoute] Guid restaurantId, [FromBody] DishCreateDto dishCreateDto) {
        await _dishService.CreateDish(restaurantId, dishCreateDto);
        return Ok();
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
    /// <param name="pageSize"></param>
    /// <param name="sort">Sorting type</param>
    /// <param name="name">Dish name for search</param>
    /// <param name="isVegetarian">Should a list of only veggie dishes be returned</param>
    /// <param name="page">Page of list (natural number)</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Pagination<DishShortDto>>> GetDishes([FromRoute] Guid restaurantId, [FromQuery] [Optional] List<Guid>? menus,
        [FromQuery] [Optional] List<DishCategory>? categories, [FromQuery] [Optional] String? name, 
        [FromQuery] Boolean isVegetarian = false, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] DishSort sort = DishSort.NameAsc) {
        return Ok(await _dishService.GetAllUnarchivedDishes(restaurantId, menus, categories, page, pageSize, name, isVegetarian, sort));
    }

    /// <summary>
    /// [Anyone] Get information about dish. If dish is archived, it is available only for Manager.
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{dishId}")]
    public async Task<ActionResult<DishFullDto>> GetDish([FromRoute] Guid dishId) {
        return Ok(await _dishService.GetDish(dishId));
    }

    /// <summary>
    /// [Manager] Edit information about dish
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="dishEditDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{dishId}/edit")]
    public async Task<ActionResult> EditDish([FromRoute] Guid dishId, [FromBody] DishEditDto dishEditDto) {
        await _dishService.EditDish(dishId, dishEditDto);
        return Ok();
    }

    /// <summary>
    /// [Manager] Archive dish
    /// </summary>
    /// <remarks>
    /// Set dish archived. It becomes available only for Manager.
    /// </remarks>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{dishId}/archive")]
    public async Task<ActionResult> ArchiveDish([FromRoute] Guid dishId) {
        await _dishService.ArchiveDish(dishId);
        return Ok();
    }
    
    /// <summary>
    /// [Manager] Get list of archived dishes
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("archived")]
    public async Task<ActionResult<List<DishShortDto>>> ArchivedDishes([FromRoute] Guid restaurantId) {
        return Ok(await _dishService.GetArchivedDishes(restaurantId));
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
    public async Task<ActionResult> UnarchiveDish([FromRoute] Guid dishId) {
        await _dishService.UnarchiveDish(dishId);
        return Ok();
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