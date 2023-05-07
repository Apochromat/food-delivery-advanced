using Delivery.Common.DTO;
using Delivery.Common.Interfaces;

namespace Delivery.BackendAPI.BL.Services; 

/// <summary>
/// Cart service
/// </summary>
public class CartService : ICartService {
    private readonly INotificationQueueService _notificationQueueService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="notificationQueueService"></param>
    public CartService(INotificationQueueService notificationQueueService) {
        _notificationQueueService = notificationQueueService;
    }

    /// <summary>
    /// Get user`s cart
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<CartDto> GetCart(Guid userId) {
        await _notificationQueueService.SendNotificationAsync(new MessageDto() {
            ReceiverId = userId,
            Title = "Cart",
            Text = "Your cart is ready!",
            CreatedAt = DateTime.UtcNow
        });
        throw new NotImplementedException();
    }

    /// <summary>
    /// Add dish to cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task AddDishToCart(Guid userId, Guid dishId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Remove dish from cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <param name="removeAll"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RemoveDishFromCart(Guid userId, Guid dishId, bool removeAll = false) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Clear cart
    /// </summary>
    /// <param name="userId"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ClearCart(Guid userId) {
        throw new NotImplementedException();
    }
}