using System.Net;
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
    /// <param name="name" example="Russian kitchen">New menu name</param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CreateRestaurantMenu([FromQuery] String name, [FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Anyone] Get list of menus in restaurant. Restaurant has Default menu always.
    /// </summary>
    /// <param name="name" example="Claude Monet">Name of menu for filter</param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult GetRestaurantMenus([FromQuery] String name, [FromRoute] Guid restaurantId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Anyone] Get information about menu, list of dishes. Default menu has all dishes from restaurant.
    /// If menu is archived, it is available only for Manager and Administrator.
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{menuId}")]
    public ActionResult GetMenu([FromRoute] Guid menuId) {
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
    public ActionResult ArchivedMenus() {
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