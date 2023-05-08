using System.Runtime.InteropServices;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for menu`s management
/// </summary>
[ApiController]
[Route("api/restaurant/{restaurantId}/menu")]
public class MenuController : ControllerBase {
    private readonly IMenuService _menuService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="menuService"></param>
    public MenuController(IMenuService menuService) {
        _menuService = menuService;
    }

    /// <summary>
    /// [Manager] Creates a new menu. You shouldn`t create menu for new Restaurant, It always has Default one. 
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="menuCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateRestaurantMenu([FromRoute] Guid restaurantId, [FromBody] MenuCreateDto menuCreateDto) {
        await _menuService.CreateRestaurantMenu(restaurantId, menuCreateDto);
        return Ok();
    }
    
    /// <summary>
    /// [Anyone] Get list of unarchived menus in restaurant. Restaurant always has Default menu.
    /// </summary>
    /// <param name="name" >Name of menu for filter</param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<MenuShortDto>>> GetRestaurantMenus([FromRoute] Guid restaurantId, [FromQuery][Optional] String? name) {
        return Ok(await _menuService.GetRestaurantMenus(name, restaurantId));
    }

    /// <summary>
    /// [Anyone] Get information about menu, list of dishes. Default menu has all dishes from restaurant.
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{menuId}")]
    public async Task<ActionResult<MenuFullDto>> GetMenu([FromRoute] Guid menuId) {
        return Ok(await _menuService.GetMenu(menuId));
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
    public async Task<ActionResult> Edit([FromRoute] Guid menuId, [FromBody] MenuEditDto menuEditDto) {
        await _menuService.EditMenu(menuId, menuEditDto);
        return Ok();
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
    public async Task<ActionResult> ArchiveMenu([FromRoute] Guid menuId) {
        await _menuService.ArchiveMenu(menuId);
        return Ok();
    }
    
    /// <summary>
    /// [Manager] Get list of archived menus.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("archived")]
    public async Task<ActionResult<List<MenuShortDto>>> ArchivedMenus([FromRoute] Guid restaurantId) {
        return Ok(await _menuService.ArchivedRestaurantMenus(restaurantId));
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
    public async Task<ActionResult> UnarchiveMenu([FromRoute] Guid menuId) {
        await _menuService.UnarchiveMenu(menuId);
        return Ok();
    }
    
    /// <summary>
    /// [Manager] Add dish to menu. Unable for Default menu.
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{menuId}/dish/{dishId}")]
    public async Task<ActionResult> AddDishToMenu([FromRoute] Guid menuId, [FromRoute] Guid dishId) {
        await _menuService.AddDishToMenu(menuId, dishId);
        return Ok();
    }
    
    /// <summary>
    /// [Manager] Remove dish from menu. Unable for Default menu.
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{menuId}/dish/{dishId}")]
    public async Task<ActionResult> RemoveDishFromMenu([FromRoute] Guid menuId, [FromRoute] Guid dishId) {
        await _menuService.RemoveDishFromMenu(menuId, dishId);
        return Ok();
    }
}