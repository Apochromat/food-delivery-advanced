using System.Text.Json;
using AspNetCoreHero.ToastNotification.Abstractions;
using Delivery.AdminPanel.Models;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Delivery.AdminPanel.Controllers;

[Controller]
public class RestaurantController : Controller {
    private readonly ILogger<RestaurantController> _logger;
    private readonly IAdminPanelRestaurantService _restaurantService;
    private readonly INotyfService _toastNotification;

    public RestaurantController(ILogger<RestaurantController> logger, IAdminPanelRestaurantService restaurantService, INotyfService toastNotification) {
        _logger = logger;
        _restaurantService = restaurantService;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Index(int page = 1) {
        var restaurants = _restaurantService.GetAllUnarchivedRestaurants(null, page, 10);
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
        if (TempData["Errors"] is Dictionary<string, string> errors) {
            foreach (var (key, value) in errors) {
                ModelState.AddModelError(key, value);
            }
        }
        
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
        // Model validation
        if (!ModelState.IsValid) { 
            _toastNotification.Error(string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            return RedirectToAction("ConcreteRestaurant", model);
        } 
        
        var errors = new Dictionary<string, string>();

        try {
            await _restaurantService.AddManagerToRestaurant(model.RestaurantId, model.Email);
            _toastNotification.Success("Manager added successfully");
            model.Email = "";
        }
        catch (NotFoundException ex) {
            _logger.LogError(ex.Message, ex);
            errors.Add("Email", ex.Message);
            _toastNotification.Error(ex.Message);
            ModelState.AddModelError("", "");
        }
        catch (MethodNotAllowedException ex) {
            _logger.LogError(ex.Message, ex);
            errors.Add("Email", ex.Message);
            _toastNotification.Error(ex.Message);
            ModelState.AddModelError("", "");
        }
        catch (Exception ex) {
            _logger.LogError(ex.Message, ex);
            errors.Add("", "Something Went Wrong");
            _toastNotification.Error(ex.Message);
            ModelState.AddModelError("", "");
        }
        
        if (!ModelState.IsValid) { 
            TempData["Errors"] = errors;
        }

        return RedirectToAction("ConcreteRestaurant", model);
    }
    
    [HttpPost]
    public async Task<IActionResult> RemoveManager(ManagerCardModel model) {
        try {
            await _restaurantService.RemoveManagerFromRestaurant(model.RestaurantId , model.Email);
            _toastNotification.Success("Manager removed successfully");
            model.Email = "";
        }
        catch (NotFoundException ex) {
            _toastNotification.Error(ex.Message);
            _logger.LogError(ex.Message, ex);
        }
        catch (MethodNotAllowedException ex) {
            _toastNotification.Error(ex.Message);
            _logger.LogError(ex.Message, ex);
        }
        catch (Exception ex) {
            _toastNotification.Error("Something went wrong");
            _logger.LogError("Something went wrong", ex);
        }

        model.Email = "";
        
        return RedirectToAction("ConcreteRestaurant", model);
    }
}