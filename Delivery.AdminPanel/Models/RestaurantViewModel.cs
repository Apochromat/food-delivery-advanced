using System.ComponentModel.DataAnnotations;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;

namespace Delivery.AdminPanel.Models; 

public class RestaurantViewModel {
    public RestaurantFullDto Restaurant { get; set; } = new();
    public List<AccountProfileFullDto> Managers { get; set; } = new();
    public List<AccountProfileFullDto> Cooks { get; set; } = new();
    public AddManagerModel Manager { get; set; } = new();
}