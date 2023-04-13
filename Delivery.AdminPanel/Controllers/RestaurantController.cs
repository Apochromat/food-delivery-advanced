using Delivery.AdminPanel.Models;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AdminPanel.Controllers;

[Controller]
public class RestaurantController : Controller {
    private readonly ILogger<RestaurantController> _logger;
    private readonly IAdminPanelRestaurantService _restaurantService;

    public RestaurantController(ILogger<RestaurantController> logger, IAdminPanelRestaurantService restaurantService) {
        _logger = logger;
        _restaurantService = restaurantService;
    }

    [HttpGet]
    [Authorize]
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
    [Authorize]
    public IActionResult ConcreteRestaurant(Guid restaurantId, AddManagerModel? manager = null) {
        var restaurant = _restaurantService.GetRestaurant(restaurantId);
        var model = new RestaurantViewModel() {
            Restaurant = restaurant,
            Managers = _restaurantService.GetRestaurantManagers(restaurantId),
            Manager = manager ?? new AddManagerModel() {
                RestaurantId = restaurant.Id
            }
        };
        return View("ConcreteRestaurant", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddManager(AddManagerModel model) {
        if (!ModelState.IsValid) {
            return RedirectToAction("ConcreteRestaurant", model);
        }
        
        await _restaurantService.AddManagerToRestaurant(model.RestaurantId, model.Email);
        return RedirectToAction("ConcreteRestaurant");
    }
}