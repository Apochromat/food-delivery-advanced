using Delivery.Common.Interfaces;
using Delivery.Notification.DAL;
using Delivery.Notification.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Notification.BL.Services;

/// <summary>
/// Connection manager service
/// </summary>
public class ConnectionManagerService : IConnectionManagerService {
    private readonly NotificationDbContext _dbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbContext"></param>
    public ConnectionManagerService(NotificationDbContext dbContext) {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Adds connection to database
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="connectionId"></param>
    public async Task AddConnectionAsync(Guid userId, string connectionId) {
        await _dbContext.Connections.AddAsync(new Connection() {
            Id = Guid.NewGuid(),
            ReceiverId = userId,
            ConnectionId = connectionId,
            CreatedAt = DateTime.UtcNow
        });
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Removes connection from database
    /// </summary>
    /// <param name="connectionId"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RemoveConnectionAsync(string connectionId) {
        var connection = await _dbContext.Connections.FirstOrDefaultAsync(c => c.ConnectionId == connectionId);
        if (connection == null) {
            return;
        }

        _dbContext.Connections.Remove(connection);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if user is connected
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> IsUserConnectedAsync(Guid userId) {
        var connections = await _dbContext.Connections.CountAsync(c => c.ReceiverId == userId);
        if (connections > 0) {
            return true;
        }

        return false;
    }
}