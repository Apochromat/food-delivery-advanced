using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class RestaurantListViewModel {
    public List<RestaurantShortDto> Restaurants { get; set; } = new List<RestaurantShortDto>();
    public RestaurantCreateModel RestaurantCreateModel { get; set; } = new RestaurantCreateModel();
    public RestaurantSearchModel RestaurantSearchModel { get; set; } = new RestaurantSearchModel();
    public int Page { get; set; }
    public int Pages { get; set; }
    public int PageSize { get; set; }
}