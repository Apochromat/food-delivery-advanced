using AspNetCoreHero.ToastNotification.Abstractions;
using Delivery.AdminPanel.Models;
using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AdminPanel.Controllers;

[Controller]
[Authorize]
public class RestaurantController : Controller {
    private readonly ILogger<RestaurantController> _logger;
    private readonly IAdminPanelRestaurantService _restaurantService;
    private readonly INotyfService _toastNotification;

    public RestaurantController(ILogger<RestaurantController> logger, IAdminPanelRestaurantService restaurantService,
        INotyfService toastNotification) {
        _logger = logger;
        _restaurantService = restaurantService;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public IActionResult Index(int page = 1, RestaurantSearchModel? searchModel = null) {
        var restaurants = _restaurantService.GetAllRestaurants(
            searchModel?.Name, page, 10, searchModel?.IsArchived, searchModel?.Sort);
        var model = new RestaurantListViewModel() {
            Restaurants = restaurants.Content,
            RestaurantCreateModel = new RestaurantCreateModel() {
                RestaurantCreateDto = new RestaurantCreateDto()
            },
            RestaurantSearchModel = searchModel ?? new RestaurantSearchModel(),
            Page = restaurants.Current,
            Pages = restaurants.Pages,
            PageSize = restaurants.Items
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult ConcreteRestaurant(Guid restaurantId, AddManagerModel? manager = null,
        AddCookModel? cook = null) {
        if (TempData["Errors"] is Dictionary<string, string> errors) {
            foreach (var (key, value) in errors) {
                ModelState.AddModelError(key, value);
            }
        }

        var restaurant = _restaurantService.GetRestaurant(restaurantId);
        var model = new RestaurantViewModel() {
            Restaurant = restaurant,
            RestaurantEditModel = new RestaurantUpdateModel() {
                RestaurantId = restaurant.Id,
                RestaurantUpdateDto = new RestaurantUpdateDto() {
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    Address = restaurant.Address,
                    BigImage = restaurant.BigImage,
                    SmallImage = restaurant.SmallImage
                }
            },
            Managers = _restaurantService.GetRestaurantManagers(restaurantId),
            Manager = manager ?? new AddManagerModel() {
                RestaurantId = restaurant.Id
            },
            Cooks = _restaurantService.GetRestaurantCooks(restaurantId),
            Cook = cook ?? new AddCookModel() {
                RestaurantId = restaurant.Id
            }
        };
        return View("ConcreteRestaurant", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddManager(AddManagerModel model) {
        // Model validation
        if (!ModelState.IsValid) {
            _toastNotification.Error(string.Join(", ",
                ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
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
            _toastNotification.Error("Something went wrong");
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
            await _restaurantService.RemoveManagerFromRestaurant(model.RestaurantId, model.Email);
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
            _logger.LogError(ex.Message, ex);
        }

        model.Email = "";

        return RedirectToAction("ConcreteRestaurant", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddCook(AddCookModel model) {
        // Model validation
        if (!ModelState.IsValid) {
            _toastNotification.Error(string.Join(", ",
                ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            return RedirectToAction("ConcreteRestaurant", model);
        }

        var errors = new Dictionary<string, string>();

        try {
            await _restaurantService.AddCookToRestaurant(model.RestaurantId, model.Email);
            _toastNotification.Success("Cook added successfully");
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
            _toastNotification.Error("Something went wrong");
        }

        if (!ModelState.IsValid) {
            TempData["Errors"] = errors;
        }

        return RedirectToAction("ConcreteRestaurant", model);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCook(CookCardModel model) {
        try {
            await _restaurantService.RemoveCookFromRestaurant(model.RestaurantId, model.Email);
            _toastNotification.Success("Cook removed successfully");
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
            _logger.LogError(ex.Message, ex);
        }

        model.Email = "";

        return RedirectToAction("ConcreteRestaurant", model);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(RestaurantCreateModel model) {
        // Model validation
        if (!ModelState.IsValid) {
            _toastNotification.Error(string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            return RedirectToAction("Index", model);
        }

        try {
            await _restaurantService.CreateRestaurant(model.RestaurantCreateDto);
            _toastNotification.Success("Restaurant created successfully");
        }
        catch (Exception ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error("Something went wrong");
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRestaurant(RestaurantUpdateModel model) {
        // Model validation
        if (!ModelState.IsValid) { 
            _toastNotification.Error(string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            return RedirectToAction("ConcreteRestaurant", model);
        }

        try {
            await _restaurantService.UpdateRestaurant(model.RestaurantId, model.RestaurantUpdateDto);
            _toastNotification.Success("Restaurant updated successfully");
        }
        catch (NotFoundException ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error(ex.Message);
        }
        catch (MethodNotAllowedException ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error(ex.Message);
        }
        catch (Exception ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error("Something went wrong");
        }

        return RedirectToAction("ConcreteRestaurant", model);
    }
    
    [HttpPost]
    public async Task<IActionResult> ArchiveRestaurant(GuidModel model) {
        try {
            await _restaurantService.ArchiveRestaurant(model.Id);
            _toastNotification.Success("Restaurant archived successfully");
        }
        catch (NotFoundException ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error(ex.Message);
        }
        catch (Exception ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error("Something went wrong");
        }

        return RedirectToAction("ConcreteRestaurant", new {restaurantId = model.Id});
    }
    
    [HttpPost]
    public async Task<IActionResult> UnarchiveRestaurant(GuidModel model) {
        try {
            await _restaurantService.UnarchiveRestaurant(model.Id);
            _toastNotification.Success("Restaurant unarchived successfully");
        }
        catch (NotFoundException ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error(ex.Message);
        }
        catch (Exception ex) {
            _logger.LogError(ex.Message, ex);
            _toastNotification.Error("Something went wrong");
        }

        return RedirectToAction("ConcreteRestaurant", new {restaurantId = model.Id});
    }
}