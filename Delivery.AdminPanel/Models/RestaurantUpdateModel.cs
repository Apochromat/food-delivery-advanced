using System.ComponentModel.DataAnnotations;
using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class RestaurantUpdateModel {
    public Guid RestaurantId { get; set; }
    [ValidateComplexType]
    public RestaurantEditDto RestaurantEditDto { get; set; } = new();
}