using Delivery.BackendAPI.DAL;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BackendAPI.BL.Services;

/// <summary>
/// Validate transaction service
/// </summary>
public class TransactionValidationService : ITransactionValidationService {
    private readonly BackendDbContext _backendDbContext;

    private readonly Dictionary<RoleType, List<Tuple<OrderStatus, OrderStatus>>> _transactions = new() {
        {
            RoleType.Cook, new() {
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Created, OrderStatus.Kitchen),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Kitchen, OrderStatus.Packaged),
            }
        }, {
            RoleType.Courier, new() {
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Packaged, OrderStatus.AssignedForCourier),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.AssignedForCourier, OrderStatus.Delivery),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Delivery, OrderStatus.Delivered),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Delivery, OrderStatus.Canceled)
            }
        }, {
            RoleType.Customer, new() {
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Created, OrderStatus.Canceled)
            }
        }, {
            RoleType.Manager, new() {
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Created, OrderStatus.Kitchen),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Created, OrderStatus.Canceled),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Kitchen, OrderStatus.Packaged),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Kitchen, OrderStatus.Canceled),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Packaged, OrderStatus.AssignedForCourier),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Packaged, OrderStatus.Canceled),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.AssignedForCourier, OrderStatus.Delivery),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.AssignedForCourier, OrderStatus.Canceled),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Delivery, OrderStatus.Delivered),
                new Tuple<OrderStatus, OrderStatus>(OrderStatus.Delivery, OrderStatus.Canceled)
            }
        }
    };

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    public TransactionValidationService(BackendDbContext backendDbContext) {
        _backendDbContext = backendDbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> ValidateTransactionAsync(RoleType role, Guid orderId, OrderStatus status) {
        var order = await _backendDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        if (order == null) {
            throw new NotFoundException("Order not found");
        }

        if (_transactions[role].Find(x => x.Item1 == order.Status && x.Item2 == status) == null) {
            throw new MethodNotAllowedException("Transaction is not allowed");
        }

        return true;
    }
}