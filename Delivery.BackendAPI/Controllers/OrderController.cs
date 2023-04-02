using System.Net;
using System.Runtime.InteropServices;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for orders management
/// </summary>
[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase {
    /// <summary>
    /// [AdminPanel, Courier] Get orders from all restaurants.
    /// </summary>
    /// <remarks>
    /// Administrator see all the orders.<br/>
    /// Courier see orders in all restaurants with statuses: Packaged.<br/>
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<Pagination<OrderShortDto>> GetAllOrders([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Cook, Courier, Customer] Get user`s orders from all restaurants.
    /// TODO: Разделить на разные эндпоинты
    /// </summary>
    /// <remarks>
    /// Cook see his orders with statuses: Kitchen, Packaged, Delivered(history of cooked orders).<br/>
    /// Courier see his orders with statuses: AssignedForCourier (assigned for him), Delivery (his deliveries).<br/>
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("mine")]
    public ActionResult<Pagination<OrderShortDto>> GetMyOrders([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [AdminPanel, Manager, Cook, Courier, Customer] Get information about specific order.
    /// </summary>
    /// <remarks>
    /// Administrator see all the orders.<br/>
    /// Manager see all the orders in his restaurant.<br/>
    /// Cook see orders in his restaurant with statuses: Created, Kitchen, Packaged, Delivered (history of orders).<br/>
    /// Courier see orders in all restaurants with statuses: Packaged, AssignedForCourier (only assigned for him), Delivery (only his deliveries).<br/>
    /// Customer see only his orders with any statuses.<br/>
    /// </remarks>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{orderId}")]
    public ActionResult<OrderFullDto> GetOrder([FromRoute] Guid orderId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Customer] Creates a new order.
    /// </summary>
    /// <remarks>
    /// Creates a new order with status Created
    /// </remarks>
    /// <param name="orderCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CreateOrder([FromBody] OrderCreateDto orderCreateDto) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Manager, Cook, Courier, Customer] Change order status.
    /// </summary>
    /// <remarks>
    /// Cook: Created_Kitchen, Kitchen_Packaged<br/>
    /// Courier: Packaged_AssignedForCourier, AssignedForCourier_Delivery, Delivery_Delivered, Delivery_Canceled<br/>
    /// Customer: Created_Canceled<br/>
    /// Manager: All of above and Any_Canceled<br/>
    /// </remarks>
    /// <param name="orderId"></param>
    /// <param name="status">Status to set</param>
    /// <returns></returns>
    [HttpPut]
    [Route("{orderId}/status")]
    public ActionResult CancelOrder([FromRoute] Guid orderId, [FromBody] OrderChangeStatusDto status) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Customer] Repeats Delivered order.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("{orderId}/repeat")]
    public ActionResult RepeatOrder([FromRoute] Guid orderId) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}