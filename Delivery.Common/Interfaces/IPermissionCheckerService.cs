using Delivery.Common.Enums;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Permission Checker service interface
/// </summary>
public interface IPermissionCheckerService {
    /// <summary>
    /// Check if user is manager of restaurant.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task<bool> IsUserManagerOfRestaurant(Guid userId, Guid restaurantId);

    /// <summary>
    /// Check if user is cook of restaurant.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    Task<bool> IsUserCookOfRestaurant(Guid userId, Guid restaurantId);

    /// <summary>
    /// Check if cook has access to order.
    /// </summary>
    /// <remarks>
    /// Cook can see all orders from his restaurant with status Created.
    /// Cook can see orders from his restaurant assigned to him with all other statuses.
    /// </remarks>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<bool> IsCookHasAccessToOrder(Guid userId, Guid orderId);

    /// <summary>
    /// Check if courier has access to order.
    /// </summary>
    /// <remarks>
    /// Courier can see all orders with status Packaged.
    /// Courier can see orders assigned to him with statuses: AssignedForCourier, Delivery, Delivered, Canceled.
    /// </remarks>>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<bool> IsCourierHasAccessToOrder(Guid userId, Guid orderId);

    /// <summary>
    /// Check if manager has access to order.
    /// </summary>
    /// <remarks>
    /// Manager can see all orders from his restaurant.
    /// </remarks>>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<bool> IsManagerHasAccessToOrder(Guid userId, Guid orderId);

    /// <summary>
    /// Check if customer has access to order.
    /// </summary>
    /// <remarks>
    /// Customer can see all his orders.
    /// </remarks>>
    /// <param name="userId"></param>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<bool> IsCustomerHasAccessToOrder(Guid userId, Guid orderId);

    /// <summary>
    /// Check if order has status from list.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="statuses"></param>
    /// <returns></returns>
    Task<bool> IsOrderHasStatus(Guid orderId, List<OrderStatus> statuses);
}