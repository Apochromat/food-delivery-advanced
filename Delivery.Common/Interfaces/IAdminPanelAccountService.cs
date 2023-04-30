using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces;

/// <summary>
/// AdminPanelAccountService interface
/// </summary>
public interface IAdminPanelAccountService {
    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="loginViewDto"></param>
    /// <returns></returns>
    Task Login(LoginViewDto loginViewDto);

    /// <summary>
    /// Logout user
    /// </summary>
    /// <returns></returns>
    Task Logout();
}