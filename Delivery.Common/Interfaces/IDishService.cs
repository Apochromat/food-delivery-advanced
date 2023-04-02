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
    /// <param name="dishCreateDto"></param>
    /// <returns></returns>
    DishFullDto CreateDish(DishCreateDto dishCreateDto);
    /// <summary>
    /// Get all unarchived dishes
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="name"></param>
    /// <param name="categories"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="isVegetarian"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Pagination<DishShortDto> GetAllUnarchivedDishes(Guid restaurantId, String name, List<DishCategory>? categories,
        int page, int pageSize = 10, Boolean isVegetarian = false, DishSort sort = DishSort.NameAsc);
    /// <summary>
    /// Get full dish info
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    DishFullDto GetDish(Guid dishId);
    /// <summary>
    /// Update dish info
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="dishUpdateDto"></param>
    /// <returns></returns>
    DishFullDto UpdateDish(Guid dishId, DishUpdateDto dishUpdateDto);
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
    List<DishShortDto> GetArchivedDishes(Guid restaurantId);
    /// <summary>
    /// Is customer able to set rating
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    Boolean IsAbleToSetRating(Guid dishId, Guid customerId);
    /// <summary>
    /// Set rating
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="customerId"></param>
    /// <param name="ratingSetDto"></param>
    /// <returns></returns>
    DishFullDto SetRating(Guid dishId, Guid customerId, RatingSetDto ratingSetDto);
}