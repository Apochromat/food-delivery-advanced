using Delivery.Common.Enums;

namespace Delivery.AdminPanel.Models; 

public class RestaurantSearchModel {
    public string? Name { get; set; }
    public RestaurantSort Sort { get; set; } = RestaurantSort.NameAsc;
    public bool? IsArchived { get; set; } = null;
    public int Page { get; set; } = 1;
}