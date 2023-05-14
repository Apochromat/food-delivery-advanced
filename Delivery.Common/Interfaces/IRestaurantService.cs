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
    Task<Pagination<RestaurantShortDto>> GetAllUnarchivedRestaurants(int page, int pageSize = 10,
        RestaurantSort sort = RestaurantSort.NameAsc, String? name = null);

    /// <summary>
    /// Get Full restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task<RestaurantFullDto> GetRestaurant(Guid restaurantId);

    /// <summary>
    /// Update restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantEditDto"></param>
    /// <returns></returns>
    Task EditRestaurant(Guid restaurantId, RestaurantEditDto restaurantEditDto);

    /// <summary>
    /// Get restaurant orders
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="sort"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Pagination<OrderShortDto>> GetRestaurantOrders(Guid restaurantId, OrderSort sort,
        List<OrderStatus>? status, String? number, int page = 1, int pageSize = 10);

    /// <summary>
    /// Get cook restaurant orders
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="sort"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<Pagination<OrderShortDto>> GetCookRestaurantOrders(Guid restaurantId, OrderSort sort,
        String? number, int page = 1, int pageSize = 10);
}