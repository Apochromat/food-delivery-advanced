using Delivery.BackendAPI.DAL;
using Delivery.Common.Enums;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BackendAPI.BL.Services; 

/// <inheritdoc/>
public class PermissionCheckerService : IPermissionCheckerService {
    private readonly BackendDbContext _backendDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    public PermissionCheckerService(BackendDbContext backendDbContext) {
        _backendDbContext = backendDbContext;
    }

    /// <inheritdoc/>
    public async Task<bool> IsUserManagerOfRestaurant(Guid userId, Guid restaurantId) {
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId && x.Managers.Contains(userId));
        if (restaurant == null) {
            return false;
        }
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> IsUserCookOfRestaurant(Guid userId, Guid restaurantId) {
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId && x.Cooks.Contains(userId));
        if (restaurant == null) {
            return false;
        }
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> IsCookHasAccessToOrder(Guid userId, Guid orderId) {
        var createdOrder = await _backendDbContext.Orders
            .Include(x => x.Restaurant)
            .FirstOrDefaultAsync(x => 
                x.Id == orderId 
                && x.Restaurant.Cooks.Contains(userId)
                && x.Status == OrderStatus.Created);
        if (createdOrder != null) {
            return true;
        }
        
        var cookingOrder = await _backendDbContext.Orders
            .FirstOrDefaultAsync(x => 
                x.Id == orderId 
                && x.CookId == userId);
        if (cookingOrder != null) {
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> IsCourierHasAccessToOrder(Guid userId, Guid orderId) {
        var packagedOrder = await _backendDbContext.Orders
            .FirstOrDefaultAsync(x => 
                x.Id == orderId 
                && x.Status == OrderStatus.Packaged);
        if (packagedOrder != null) {
            return true;
        }
        
        var courierOrder = await _backendDbContext.Orders
            .FirstOrDefaultAsync(x => 
                x.Id == orderId 
                && x.CourierId == userId);
        if (courierOrder != null) {
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> IsManagerHasAccessToOrder(Guid userId, Guid orderId) {
        var managerOrder = await _backendDbContext.Orders
            .Include(x => x.Restaurant)
            .FirstOrDefaultAsync(x => 
                x.Id == orderId 
                && x.Restaurant.Managers.Contains(userId));
        if (managerOrder != null) {
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> IsCustomerHasAccessToOrder(Guid userId, Guid orderId) {
        var customerOrder = await _backendDbContext.Orders
            .FirstOrDefaultAsync(x => 
                x.Id == orderId 
                && x.CustomerId == userId);
        if (customerOrder != null) {
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public async Task<bool> IsOrderHasStatus(Guid orderId, List<OrderStatus> statuses) {
        var order = await _backendDbContext.Orders
            .FirstOrDefaultAsync(x => 
                x.Id == orderId 
                && statuses.Contains(x.Status));
        if (order != null) {
            return true;
        }

        return false;
    }
}