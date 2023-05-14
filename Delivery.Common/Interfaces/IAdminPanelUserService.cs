using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Admin panel user service
/// </summary>
public interface IAdminPanelUserService {
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    Task<Pagination<AccountProfileFullDto>> GetAllUsers(string? name, int page, int pageSize = 10);

    /// <summary>
    /// Edit user profile
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task EditUser(Guid userId, AdminPanelAccountProfileEditDto model);

    /// <summary>
    /// Is user banned
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> IsUserBanned(Guid userId);

    /// <summary>
    /// Ban user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task BanUser(Guid userId);

    /// <summary>
    /// Unban user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task UnbanUser(Guid userId);
}