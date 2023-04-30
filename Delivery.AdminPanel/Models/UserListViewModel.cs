using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class UserListViewModel {
    public List<AccountProfileFullDto> Users { get; set; } = new();
    public int Page { get; set; }
    public int Pages { get; set; }
    public int PageSize { get; set; }
}