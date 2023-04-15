using System.ComponentModel.DataAnnotations;
using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class RestaurantCreateModel {
    [ValidateComplexType]
    public RestaurantCreateDto RestaurantCreateDto { get; set; } = new();
}