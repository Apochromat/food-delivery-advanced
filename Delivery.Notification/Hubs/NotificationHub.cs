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
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public NotificationHub(ILogger<NotificationHub> logger) {
        _logger = logger;
    }
    
    /// <summary>
    /// User connection event
    /// </summary>
    public override async Task OnConnectedAsync() {
        await Groups.AddToGroupAsync(Context.ConnectionId, Context.UserIdentifier);
        await Clients.Group(Context.UserIdentifier).SendAsync("ReceiveMessage", $"Welcome to the notification hub!");
        await base.OnConnectedAsync();
    }
}