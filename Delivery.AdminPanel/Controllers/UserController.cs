using Delivery.AdminPanel.Models;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AdminPanel.Controllers;

public class UserController : Controller {
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger) {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index() {
        return View();
    }
}