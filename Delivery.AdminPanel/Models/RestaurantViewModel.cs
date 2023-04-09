using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class RestaurantViewModel {
    /// <summary>
    /// Restaurant
    /// </summary>
    public RestaurantFullDto Restaurant { get; set; }
    public List<AccountProfileFullDto> Users { get; set; } = new();
    public List<AccountProfileFullDto> Managers { get; set; } = new();
    public List<AccountProfileFullDto> Cooks { get; set; } = new();
}