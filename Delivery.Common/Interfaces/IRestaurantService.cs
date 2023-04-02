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
    List<RestaurantShortDto> GetAllRestaurants();
}