using Delivery.Common.DTO;
using Delivery.Common.Enums;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Dish service interface
/// </summary>
public interface IDishService {
    /// <summary>
    /// Create new dish
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="dishCreateDto"></param>
    /// <returns></returns>
    Task CreateDish(Guid restaurantId, DishCreateDto dishCreateDto);

    /// <summary>
    /// Get all unarchived dishes
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="name"></param>
    /// <param name="menus"></param>
    /// <param name="categories"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="isVegetarian"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Task<Pagination<DishShortDto>> GetAllUnarchivedDishes(Guid restaurantId, List<Guid>? menus,
        List<DishCategory>? categories, int page, int pageSize = 10, string? name = null,
        bool? isVegetarian = null, DishSort sort = DishSort.NameAsc);

    /// <summary>
    /// Get full dish info
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    Task<DishFullDto> GetDish(Guid dishId);

    /// <summary>
    /// Update dish info
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="dishUpdateDto"></param>
    /// <returns></returns>
    Task EditDish(Guid dishId, DishEditDto dishUpdateDto);

    /// <summary>
    /// Archive dish
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    Task ArchiveDish(Guid dishId);

    /// <summary>
    /// Unarchive dish
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    Task UnarchiveDish(Guid dishId);

    /// <summary>
    /// Get list of archived dishes
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task<List<DishShortDto>> GetArchivedDishes(Guid restaurantId);
}