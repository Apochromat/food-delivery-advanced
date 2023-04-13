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
[Route("api")]
public class OrderController : ControllerBase {
    /// <summary>
    /// [Courier] Get orders from all restaurants for courier.
    /// </summary>
    /// <remarks>
    /// Courier see orders in all restaurants with statuses: Packaged.<br/>
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/courier/avaliable")]
    public ActionResult<Pagination<OrderShortDto>> GetAllPackagedOrders([FromQuery] [Optional] String? number, 
        [FromQuery] int page = 1,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Cook] Get cooks`s orders for work with them.
    /// </summary>
    /// <remarks>
    /// Cook see his orders with statuses: Kitchen, Packaged.
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/cook/current")]
    public ActionResult<Pagination<OrderShortDto>> GetCookCurrentOrders([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Courier] Get courier`s orders for work with them.
    /// </summary>
    /// <remarks>
    /// Courier see his orders with statuses: AssignedForCourier, Delivery.
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/courier/current")]
    public ActionResult<Pagination<OrderShortDto>> GetCourierCurrentOrders([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Customer] Get customer`s current orders.
    /// </summary>
    /// <remarks>
    /// Customer see his orders with statuses: Created, Kitchen, Packaged, AssignedForCourier, Delivery.
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/customer/current")]
    public ActionResult<Pagination<OrderShortDto>> GetCustomerCurrentOrders([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Cook] Get cook`s orders history.
    /// </summary>
    /// <remarks>
    /// Cook see his orders with statuses: AssignedForCourier, Delivery, Delivered, Canceled.
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/cook/history")]
    public ActionResult<Pagination<OrderShortDto>> GetCookOrdersHistory([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// Get courier`s orders history.
    /// </summary>
    /// <remarks>
    /// [Courier] Courier see his orders with statuses: Delivered, Canceled.
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/courier/history")]
    public ActionResult<Pagination<OrderShortDto>> GetCourierOrdersHistory([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
    
    /// <summary>
    /// [Customer] Get customer`s orders history.
    /// </summary>
    /// <remarks>
    /// Customer see his orders with statuses: Delivered, Canceled.
    /// </remarks>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/customer/history")]
    public ActionResult<Pagination<OrderShortDto>> GetCustomerOrdersHistory([FromQuery] [Optional] List<OrderStatus>? status, 
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, 
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Manager, Cook, Courier, Customer] Get information about specific order.
    /// </summary>
    /// <remarks>
    /// User see his order information with resource based access.
    /// </remarks>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("order/{orderId}")]
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
    [Route("order")]
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
    [Route("order/{orderId}/status")]
    public ActionResult CancelOrder([FromRoute] Guid orderId, [FromBody] OrderChangeStatusDto status) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }

    /// <summary>
    /// [Customer] Repeats Delivered order, by adding all dishes of them to cart.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="replaceCart">If true, clear cart and add dishes. Else only add dishes</param>
    /// <returns></returns>
    [HttpPost]
    [Route("order/{orderId}/repeat")]
    public ActionResult RepeatOrder([FromRoute] Guid orderId, Boolean? replaceCart = false) {
        return Problem("Not Implemented", "Not Implemented", (int)HttpStatusCode.NotImplemented);
    }
}