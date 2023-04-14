using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class RestaurantViewModel {
    public RestaurantFullDto Restaurant { get; set; } = new();
    public RestaurantUpdateModel RestaurantEditModel { get; set; } = new();
    public List<AccountProfileFullDto> Managers { get; set; } = new();
    public List<AccountProfileFullDto> Cooks { get; set; } = new();
    public AddManagerModel Manager { get; set; } = new();
    public AddCookModel Cook { get; set; } = new(); 
}