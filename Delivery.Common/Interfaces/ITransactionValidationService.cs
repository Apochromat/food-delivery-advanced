using Delivery.Common.Enums;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Interface for the transaction validation service.
/// </summary>
public interface ITransactionValidationService {
    /// <summary>
    /// Validates ability to change order status.
    /// </summary>
    /// <param name="role"></param>
    /// <param name="orderId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<bool> ValidateTransactionAsync(RoleType role, Guid orderId, OrderStatus status);
}