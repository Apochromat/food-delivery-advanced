using Microsoft.AspNetCore.Mvc;

namespace Delivery.Notification.Controllers;

[ApiController]
[Route("[controller]")]
public class NotifyController : ControllerBase {
    private readonly ILogger<NotifyController> _logger;

    public NotifyController(ILogger<NotifyController> logger) {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Connect() {
        return Ok();
    }
}