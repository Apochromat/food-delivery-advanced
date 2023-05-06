using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Interface for notification queue service
/// </summary>
public interface INotificationQueueService {
    /// <summary>
    /// Sends notification to user
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    Task SendNotificationAsync(MessageDto messageDto);
}