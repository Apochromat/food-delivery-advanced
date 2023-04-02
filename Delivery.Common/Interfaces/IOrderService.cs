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
    /// <param name="sort"></param>
    /// <returns></returns>
    Pagination<OrderShortDto> GetAllOrders(List<OrderStatus>? status, String? number,  int page = 1, 
        OrderSort sort = OrderSort.CreationDesc);
    /// <summary>
    /// Get Customer`s orders from all restaurants.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Pagination<OrderShortDto> GetMyCustomerOrders(Guid customerId, List<OrderStatus>? status, String? number,  int page = 1, 
        OrderSort sort = OrderSort.CreationDesc);
    /// <summary>
    /// Get Cook`s orders.
    /// </summary>
    /// <param name="cookId"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Pagination<OrderShortDto> GetMyCookOrders(Guid cookId, List<OrderStatus>? status, String? number,  int page = 1, 
        OrderSort sort = OrderSort.CreationDesc);
    /// <summary>
    /// Get Courier`s orders.
    /// </summary>
    /// <param name="courierId"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    Pagination<OrderShortDto> GetMyCourierOrders(Guid courierId, List<OrderStatus>? status, String? number,  int page = 1, 
        OrderSort sort = OrderSort.CreationDesc);
    /// <summary>
    /// Get information about specific order.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    OrderFullDto GetOrder(Guid orderId);
    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    OrderFullDto CreateOrder(OrderCreateDto order);
    /// <summary>
    /// Set order status.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    OrderFullDto SetOrderStatus(Guid orderId, OrderStatus status);
    /// <summary>
    /// Set order courier.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="courierId"></param>
    /// <returns></returns>
    OrderFullDto SetOrderCourier(Guid orderId, Guid courierId);
    /// <summary>
    /// Set order cook.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="cookId"></param>
    /// <returns></returns>
    OrderFullDto SerOrderCook(Guid orderId, Guid cookId);
    /// <summary>
    /// Repeats order.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    OrderFullDto RepeatOrder(Guid orderId);
}