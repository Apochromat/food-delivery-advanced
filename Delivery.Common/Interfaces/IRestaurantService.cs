using Delivery.Common.DTO;
using Delivery.Common.Enums;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Restaurant service interface
/// </summary>
public interface IRestaurantService {
    /// <summary>
    /// Get all restaurants
    /// </summary>
    /// <returns></returns>
    Pagination<RestaurantShortDto> GetAllUnarchivedRestaurants(String name, int page, int pageSize = 10);

    /// <summary>
    /// Get Full restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    RestaurantFullDto GetRestaurant(Guid restaurantId);

    /// <summary>
    /// Create new restaurant
    /// </summary>
    /// <param name="restaurantCreateDto"></param>
    /// <returns></returns>
    RestaurantFullDto CreateRestaurant(RestaurantCreateDto restaurantCreateDto);

    /// <summary>
    /// Update restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantUpdateDto"></param>
    /// <returns></returns>
    RestaurantFullDto UpdateRestaurant(Guid restaurantId, RestaurantUpdateDto restaurantUpdateDto);

    /// <summary>
    /// Archive restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    RestaurantFullDto ArchiveRestaurant(Guid restaurantId);

    /// <summary>
    /// Unarchive restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    RestaurantFullDto UnarchiveRestaurant(Guid restaurantId);

    /// <summary>
    /// Get list of archived restaurants
    /// </summary>
    /// <returns></returns>
    List<RestaurantShortDto> GetArchivedRestaurants();

    /// <summary>
    /// Get restaurant orders
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="sort"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    Pagination<OrderShortDto> GetRestaurantOrders(Guid restaurantId, OrderSort sort,
        List<OrderStatus>? status, String? number, int page = 1);
}