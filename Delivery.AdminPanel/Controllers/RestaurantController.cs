using Delivery.AdminPanel.Models;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AdminPanel.Controllers;

public class RestaurantController : Controller {
    private readonly ILogger<RestaurantController> _logger;
    private readonly IAdminPanelRestaurantService _restaurantService;

    public RestaurantController(ILogger<RestaurantController> logger, IAdminPanelRestaurantService restaurantService) {
        _logger = logger;
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public IActionResult Index() {
        var restaurants = _restaurantService.GetAllUnarchivedRestaurants(null, 1, 10);
        var model = new RestaurantUnarchivedListViewModel() {
            Restaurants = restaurants.Content,
            Page = restaurants.Current,
            Pages = restaurants.Pages,
            PageSize = restaurants.Items
        };
        return View(model);
    }
    
    [HttpGet]
    public IActionResult ConcreteRestaurant(Guid restaurantId) {
        var restaurant = _restaurantService.GetRestaurant(restaurantId);
        var model = new RestaurantViewModel() {
            Restaurant = restaurant,
            Managers = _restaurantService.GetRestaurantManagers(restaurantId)
        };
        return View(model);
    }
    
}