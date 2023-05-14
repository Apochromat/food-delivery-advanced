namespace Delivery.Common.Enums;

/// <summary>
/// Status names for orders
/// </summary>
public enum OrderStatus {
    /// <summary>
    /// Set when the order is just created and not accepted by the Сook
    /// </summary>
    Created,

    /// <summary>
    /// Set when the Cook started to make the order
    /// </summary>
    Kitchen,

    /// <summary>
    /// Set when an order is ready but not yet assigned to a specific Courier
    /// </summary>
    Packaged,

    /// <summary>
    /// Set when the order is assigned to a specific Courier and is waiting for him in the restaurant
    /// </summary>
    AssignedForCourier,

    /// <summary>
    /// Set when the Courier took the order from the restaurant and went to deliver it
    /// </summary>
    Delivery,

    /// <summary>
    /// Set when the order is successfully delivered
    /// </summary>
    Delivered,

    /// <summary>
    /// Set when the order has been canceled by the Customer or Courier
    /// </summary>
    Canceled
}