﻿using Delivery.Common.DTO;
using Delivery.Common.Enums;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Admin panel restaurant service
/// </summary>
public interface IAdminPanelRestaurantService {
    /// <summary>
    /// Get all restaurants
    /// </summary>
    /// <returns></returns>
    Pagination<RestaurantShortDto> GetAllRestaurants(String? name, int page, int pageSize = 10, bool? isArchived = null,
        RestaurantSort? sort = null);

    /// <summary>
    /// Get Full restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    RestaurantFullDto GetRestaurant(Guid restaurantId);

    /// <summary>
    /// Get restaurant manager list
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    List<AccountProfileFullDto> GetRestaurantManagers(Guid restaurantId);

    /// <summary>
    /// Get restaurant cook list
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    List<AccountProfileFullDto> GetRestaurantCooks(Guid restaurantId);

    /// <summary>
    /// Create new restaurant
    /// </summary>
    /// <param name="restaurantCreateDto"></param>
    /// <returns></returns>
    Task CreateRestaurant(RestaurantCreateDto restaurantCreateDto);

    /// <summary>
    /// Update restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantEditDto"></param>
    /// <returns></returns>
    Task UpdateRestaurant(Guid restaurantId, RestaurantEditDto restaurantEditDto);

    /// <summary>
    /// Archive restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task ArchiveRestaurant(Guid restaurantId);

    /// <summary>
    /// Unarchive restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task UnarchiveRestaurant(Guid restaurantId);

    /// <summary>
    /// Add manager to restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    Task AddManagerToRestaurant(Guid restaurantId, String email);

    /// <summary>
    /// Remove manager from restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    Task RemoveManagerFromRestaurant(Guid restaurantId, String email);

    /// <summary>
    /// Add cook to restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    Task AddCookToRestaurant(Guid restaurantId, String email);

    /// <summary>
    /// Remove cook from restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    Task RemoveCookFromRestaurant(Guid restaurantId, string email);
}