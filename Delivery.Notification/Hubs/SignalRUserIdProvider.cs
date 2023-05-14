using Microsoft.AspNetCore.SignalR;

namespace Delivery.Notification.Hubs;

/// <summary>
/// Provides the User ID for SignalR
/// </summary>
public class SignalRUserIdProvider : IUserIdProvider {
    /// <summary>
    /// Returns the User ID for SignalR
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    public string GetUserId(HubConnectionContext connection) {
        return connection.User.Identity!.Name!;
    }
}