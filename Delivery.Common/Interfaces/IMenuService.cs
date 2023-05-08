using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Menu service interface
/// </summary>
public interface IMenuService {
    /// <summary>
    /// Create new menu
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="menuCreateDto"></param>
    /// <returns></returns>
    Task CreateRestaurantMenu(Guid restaurantId, MenuCreateDto menuCreateDto);
    /// <summary>
    /// Get all unarchived menus
    /// </summary>
    /// <param name="name"></param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task<List<MenuShortDto>> GetRestaurantMenus(String? name, Guid restaurantId);
    /// <summary>
    /// Get full menu info
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    Task<MenuFullDto> GetMenu(Guid menuId);
    /// <summary>
    /// Archive menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    Task ArchiveMenu(Guid menuId);
    /// <summary>
    /// Unarchive menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    Task UnarchiveMenu(Guid menuId);
    /// <summary>
    /// Get list of archived menus
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task<List<MenuShortDto>> ArchivedRestaurantMenus(Guid restaurantId);
    /// <summary>
    /// Add dish to menu. Unable for Default menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <returns></returns>
    Task AddDishToMenu(Guid menuId, Guid dishId);
    /// <summary>
    /// Remove dish from menu. Unable for Default menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <returns></returns>
    Task RemoveDishFromMenu(Guid menuId, Guid dishId);
    /// <summary>
    /// Update menu info
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="menuEditDto"></param>
    /// <returns></returns>
    Task EditMenu(Guid menuId, MenuEditDto menuEditDto);
}