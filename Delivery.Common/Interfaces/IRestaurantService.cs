using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Restaurant service interface
/// </summary>
public interface IRestaurantService {
    /// <summary>
    /// Get all restaurants
    /// </summary>
    /// <returns></returns>
    Pagination<RestaurantShortDto> GetAllRestaurants(String name, int page, int pageSize = 10);
    RestaurantFullDto GetRestaurant(Guid restaurantId);
    RestaurantFullDto CreateRestaurant(RestaurantCreateDto restaurantCreateDto);
    RestaurantFullDto UpdateRestaurant(Guid restaurantId, RestaurantUpdateDto restaurantUpdateDto);
    void DeleteRestaurant(Guid restaurantId);
    
}