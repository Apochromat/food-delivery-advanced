using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Delivery.Notification.Hubs; 

/// <summary>
/// Hub for notifications
/// </summary>
[SignalRHub("/api/notifications")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class NotificationHub : Hub {
    private readonly ILogger<NotificationHub> _logger;
    private readonly IConnectionManagerService _connectionManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="connectionManager"></param>
    public NotificationHub(ILogger<NotificationHub> logger, IConnectionManagerService connectionManager) {
        _logger = logger;
        _connectionManager = connectionManager;
    }
    
    /// <summary>
    /// User connection event
    /// </summary>
    public override async Task OnConnectedAsync() {
        if (Context.UserIdentifier == null) {
            _logger.LogError("User identifier is null");
            return;
        }
        await _connectionManager.AddConnectionAsync(new Guid(Context.UserIdentifier), Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, Context.UserIdentifier);
        await base.OnConnectedAsync();
    }

    /// <summary>
    /// User disconnection event
    /// </summary>
    /// <param name="exception"></param>
    public override async Task OnDisconnectedAsync(Exception? exception) {
        if (Context.UserIdentifier == null) {
            _logger.LogError("User identifier is null");
            return;
        }
        await _connectionManager.RemoveConnectionAsync(Context.ConnectionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.UserIdentifier);
        await base.OnDisconnectedAsync(exception);
    }
}