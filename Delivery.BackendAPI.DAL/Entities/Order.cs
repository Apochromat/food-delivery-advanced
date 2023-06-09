﻿using Delivery.Common.Enums;

namespace Delivery.BackendAPI.DAL.Entities;

/// <summary>
/// Order entity
/// </summary>
public class Order {
    /// <summary>
    /// Order Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Number of the order 
    /// </summary>
    public required string Number { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Customer comment to order
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Time when order should be delivered
    /// </summary>
    public DateTime DeliveryTime { get; set; }

    /// <summary>
    /// Time when order was created
    /// </summary>
    public DateTime OrderTime { get; set; }

    /// <summary>
    /// Time when order was delivered
    /// </summary>
    public DateTime DeliveredTime { get; set; }

    /// <summary>
    /// Total price of the order
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Address of the order
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Order dishes
    /// </summary>
    public List<OrderDish> Dishes { get; set; } = new List<OrderDish>();

    /// <summary>
    /// Restaurant of the order
    /// </summary>
    public required Restaurant Restaurant { get; set; }

    /// <summary>
    /// Id of the assigned cook
    /// </summary>
    public Guid? CookId { get; set; }

    /// <summary>
    /// Id of the customer
    /// </summary>
    public required Guid CustomerId { get; set; }

    /// <summary>
    /// Id of the assigned courier
    /// </summary>
    public Guid? CourierId { get; set; }
}