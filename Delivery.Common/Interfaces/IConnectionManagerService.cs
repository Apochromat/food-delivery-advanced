namespace Delivery.Common.Interfaces; 

/// <summary>
/// Interface for connection manager service
/// </summary>
public interface IConnectionManagerService {
    /// <summary>
    /// Starts connection tracking
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="connectionId"></param>
    /// <returns></returns>
    Task AddConnectionAsync(Guid userId, string connectionId);
    /// <summary>
    /// Ends connection tracking
    /// </summary>
    /// <param name="connectionId"></param>
    /// <returns></returns>
    Task RemoveConnectionAsync(string connectionId);
    /// <summary>
    /// Checks if user is connected
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> IsUserConnectedAsync(Guid userId);
}