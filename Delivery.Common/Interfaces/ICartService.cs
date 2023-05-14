using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Cart service interface
/// </summary>
public interface ICartService {
    /// <summary>
    /// Clear cart from archived dishes
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task ClearCartFromArchivedDishes(Guid userId);

    /// <summary>
    /// Get user cart
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CartDto> GetCart(Guid userId);

    /// <summary>
    /// Add dish to cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Task AddDishToCart(Guid userId, Guid dishId, int amount = 1);

    /// <summary>
    /// Remove dish from cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <param name="removeAll"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Task RemoveDishFromCart(Guid userId, Guid dishId, Boolean removeAll = false, int amount = 1);

    /// <summary>
    /// Clear cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="force"></param>
    /// <returns></returns>
    Task ClearCart(Guid userId, bool force = true);
}