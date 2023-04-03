using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces; 

/// <summary>
/// Service for account management
/// </summary>
public interface IAccountService {
    Task<AccountProfileFullDto> GetProfileAsync(Guid userId);
}