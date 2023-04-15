using AspNetCoreHero.ToastNotification.Abstractions;
using Delivery.AdminPanel.Models;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AdminPanel.Controllers;

public class UserController : Controller {
    private readonly ILogger<UserController> _logger;
    private readonly IAdminPanelUserService _adminPanelUserService;
    private readonly INotyfService _toastNotification;

    public UserController(ILogger<UserController> logger, IAdminPanelUserService adminPanelUserService, INotyfService toastNotification) {
        _logger = logger;
        _adminPanelUserService = adminPanelUserService;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public async Task<IActionResult> Index() {
        var users = await _adminPanelUserService.GetAllUsers(null, 1);
        
        var model = new UserListViewModel() {
            Users = users.Content,
            Page = users.Current,
            Pages = users.Pages,
            PageSize = users.Items
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UserEditModel model) {
        try {
            await _adminPanelUserService.EditUser(model.Id, new AdminPanelAccountProfileEditDto() {
                FullName = model.FullName,
                Gender = model.Gender,
                Roles = model.Roles
            });
            _toastNotification.Success("User edited successfully");
        }
        catch (Exception e) {
            _logger.LogError(e, "Error while editing user");
            _toastNotification.Error("Something went wrong");
        }
        return RedirectToAction("Index");
    }
}