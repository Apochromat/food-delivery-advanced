using Delivery.AdminPanel.Models;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AdminPanel.Controllers;

public class RestaurantController : Controller {
    private readonly ILogger<RestaurantController> _logger;

    public RestaurantController(ILogger<RestaurantController> logger) {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index() {
        return View();
    }
}