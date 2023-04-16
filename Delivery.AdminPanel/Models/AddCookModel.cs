using System.ComponentModel.DataAnnotations;

namespace Delivery.AdminPanel.Models; 

public class AddCookModel {
    [Required]
    [EmailAddress]
    public String? Email { get; set; }
    public Guid RestaurantId { get; set; }
}