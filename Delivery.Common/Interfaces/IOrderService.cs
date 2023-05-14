using Delivery.Common.DTO;
using Delivery.Common.Enums;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Order service interface
/// </summary>
public interface IOrderService {
    /// <summary>
    /// Get all orders from all restaurants.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Task<Pagination<OrderShortDto>> GetAllOrders(List<OrderStatus>? status = null, String? number = null, int page = 1,
        int pageSize = 10, OrderSort sort = OrderSort.CreationDesc);

    /// <summary>
    /// Get Customer`s orders from all restaurants.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Task<Pagination<OrderShortDto>> GetMyCustomerOrders(Guid customerId, List<OrderStatus>? status = null,
        String? number = null, int page = 1, int pageSize = 10, OrderSort sort = OrderSort.CreationDesc);

    /// <summary>
    /// Get Cook`s orders.
    /// </summary>
    /// <param name="cookId"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Task<Pagination<OrderShortDto>> GetMyCookOrders(Guid cookId, List<OrderStatus>? status, String? number,
        int page = 1, int pageSize = 10, OrderSort sort = OrderSort.CreationDesc);

    /// <summary>
    /// Get Courier`s orders.
    /// </summary>
    /// <param name="courierId"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Task<Pagination<OrderShortDto>> GetMyCourierOrders(Guid courierId, List<OrderStatus>? status, String? number,
        int page = 1, int pageSize = 10, OrderSort sort = OrderSort.CreationDesc);

    /// <summary>
    /// Get information about specific order.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<OrderFullDto> GetOrder(Guid orderId);

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="orderCreateDto"></param>
    /// <returns></returns>
    Task CreateOrder(Guid customerId, OrderCreateDto orderCreateDto);

    /// <summary>
    /// Set order status.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="status"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task SetOrderStatus(Guid orderId, OrderStatus status, Guid? userId = null);

    /// <summary>
    /// Repeats order.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="force"></param>
    /// <returns></returns>
    Task RepeatOrder(Guid orderId, bool force = false);
}