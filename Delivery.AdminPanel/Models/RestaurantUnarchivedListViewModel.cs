using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class RestaurantUnarchivedListViewModel {
    public List<RestaurantShortDto> Restaurants { get; set; } = new List<RestaurantShortDto>();
    public int Page { get; set; }
    public int Pages { get; set; }
    public int PageSize { get; set; }
}