using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// AdminPanelAccountService interface
/// </summary>
public interface IAdminPanelAccountService {
    Task Login(LoginViewDto loginViewDto);
    Task Logout();
}