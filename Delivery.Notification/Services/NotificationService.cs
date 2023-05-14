using System.Text.Json;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Delivery.Notification.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Delivery.Notification.Services;

/// <summary>
/// Service for sending notifications
/// </summary>
public class NotificationService : INotificationService {
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IConnectionManagerService _connectionManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hubContext"></param>
    /// <param name="connectionManager"></param>
    public NotificationService(IHubContext<NotificationHub> hubContext, IConnectionManagerService connectionManager) {
        _hubContext = hubContext;
        _connectionManager = connectionManager;
    }

    /// <summary>
    /// Sends notification to user
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> SendNotificationAsync(MessageDto messageDto) {
        if (await _connectionManager.IsUserConnectedAsync(messageDto.ReceiverId)) {
            await _hubContext.Clients.Group(messageDto.ReceiverId.ToString())
                .SendAsync("ReceiveMessage", JsonSerializer.Serialize(messageDto));
            return true;
        }

        return false;
    }
}