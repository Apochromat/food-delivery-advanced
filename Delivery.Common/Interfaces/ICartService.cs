using Delivery.Common.DTO;
using Delivery.Common.Enums;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Cart service interface
/// </summary>
public interface ICartService {
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
    /// <returns></returns>
    Task AddDishToCart(Guid userId, Guid dishId);
    /// <summary>
    /// Remove dish from cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <param name="removeAll"></param>
    /// <returns></returns>
    Task RemoveDishFromCart(Guid userId, Guid dishId, Boolean removeAll = false);
    /// <summary>
    /// Clear cart
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task ClearCart(Guid userId);
}