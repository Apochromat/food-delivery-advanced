using Delivery.AdminPanel.Models;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.AdminPanel.Controllers;

[Controller]
public class AccountController : Controller {
    private readonly IAdminPanelAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAdminPanelAccountService accountService, ILogger<AccountController> logger) {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login() {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model) {
        if (ModelState.IsValid) {
            try {
                await _accountService.Login(new LoginViewDto() {
                    Email = model.Email,
                    Password = model.Password
                });
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex) {
                ModelState.AddModelError("Errors", ex.Message);
            }
        }

        return View(model);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout() {
        await _accountService.Logout();
        return RedirectToAction("Index", "Home");
    }
}