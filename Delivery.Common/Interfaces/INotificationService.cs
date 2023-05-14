using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Interface for notification service
/// </summary>
public interface INotificationService {
    /// <summary>
    /// Sends notification to user
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    Task<bool> SendNotificationAsync(MessageDto messageDto);
}