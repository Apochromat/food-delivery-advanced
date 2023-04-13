using System.Net;
using Delivery.Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for menu`s management
/// </summary>
[ApiController]
[Route("api/restaurant/{restaurantId}/menu")]
public class MenuController : ControllerBase {
    /// <summary>
    /// [Manager] Creates a new menu. You shouldn`t create menu for new Restaurant, It always has Default one. 
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CreateRestaurantMenu([FromRoute] Guid restaurantId, [FromBody] RestaurantCreateDto restaurantCreateDto) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Anyone] Get list of unarchived menus in restaurant. Restaurant always has Default menu.
    /// </summary>
    /// <param name="name" example="Claude Monet">Name of menu for filter</param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<MenuShortDto>> GetRestaurantMenus([FromQuery] String name, [FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Anyone] Get information about menu, list of dishes. Default menu has all dishes from restaurant.
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{menuId}")]
    public ActionResult<MenuFullDto> GetMenu([FromRoute] Guid menuId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Manager] Update menu. Unable for Default menu.
    /// </summary>
    /// <remarks>
    /// Update menu name.
    /// </remarks>
    /// <param name="menuId"></param>
    /// <param name="menuEditDto"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{menuId}")]
    public ActionResult Update([FromRoute] Guid menuId, [FromBody] MenuEditDto menuEditDto) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Manager] Archive menu. Unable for Default menu.
    /// </summary>
    /// <remarks>
    /// Set menu archived. It becomes available only for Manager and Administrator in Archived menus list.
    /// </remarks>
    /// <param name="menuId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{menuId}/archive")]
    public ActionResult ArchiveMenu([FromRoute] Guid menuId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Manager] Get list of archived menus.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("archived")]
    public ActionResult<List<MenuShortDto>> ArchivedMenus() {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Manager] Unarchive menu. Only for archived menus.
    /// </summary>
    /// <remarks>
    /// Set menu unarchived. It becomes available for all users.
    /// </remarks>>
    /// <param name="menuId"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{menuId}/unarchive")]
    public ActionResult UnarchiveMenu([FromRoute] Guid menuId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Manager] Add dish to menu. Unable for Default menu.
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{menuId}/add-dish/{dishId}")]
    public ActionResult AddDishToMenu([FromRoute] Guid menuId, [FromRoute] Guid dishId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Manager] Remove dish from menu. Unable for Default menu.
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{menuId}/delete-dish/{dishId}")]
    public ActionResult RemoveDishFromMenu([FromRoute] Guid menuId, [FromRoute] Guid dishId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}