using System.Runtime.InteropServices;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.BackendAPI.Controllers;

/// <summary>
/// Controller for orders management
/// </summary>
[ApiController]
[Route("api")]
public class OrderController : ControllerBase {
    private readonly IOrderService _orderService;
    private readonly IPermissionCheckerService _permissionCheckerService;

    //TODO: Add manager`s orders endpoints
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="orderService"></param>
    /// <param name="permissionCheckerService"></param>
    public OrderController(IOrderService orderService, IPermissionCheckerService permissionCheckerService) {
        _orderService = orderService;
        _permissionCheckerService = permissionCheckerService;
    }

    /// <summary>
    /// [Courier] Get orders from all restaurants for courier.
    /// </summary>
    /// <remarks>
    /// Courier see orders in all restaurants with statuses: Packaged.<br/>
    /// </remarks>
    /// <param name="number">Order number for search</param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/courier/avaliable")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetAllPackagedOrders(
        [FromQuery] [Optional] String? number,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        return Ok(await _orderService.GetAllOrders(
            new List<OrderStatus>() { OrderStatus.Packaged }, number, page, pageSize, sort));
    }

    /// <summary>
    /// [Courier] Get courier`s orders for work with them.
    /// </summary>
    /// <remarks>
    /// Courier see his orders with statuses: AssignedForCourier, Delivery.
    /// </remarks>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/courier/current")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCourierCurrentOrders(
        [FromQuery] [Optional] List<OrderStatus>? status,
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        var allowedStatuses = new List<OrderStatus>() { OrderStatus.AssignedForCourier, OrderStatus.Delivery };
        if (status != null && !status.All(x => allowedStatuses.Contains(x))) {
            throw new BadRequestException("Statuses must be: AssignedForCourier, Delivery");
        }

        return Ok(await _orderService.GetMyCourierOrders(userId, status ?? allowedStatuses, number, page, pageSize, sort));
    }

    /// <summary>
    /// Get courier`s orders history.
    /// </summary>
    /// <remarks>
    /// [Courier] Courier see his orders with statuses: Delivered, Canceled.
    /// </remarks>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/courier/history")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCourierOrdersHistory(
        [FromQuery] [Optional] List<OrderStatus>? status,
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        var allowedStatuses = new List<OrderStatus>() { OrderStatus.Delivered, OrderStatus.Canceled };
        if (status != null && !status.All(x => allowedStatuses.Contains(x))) {
            throw new BadRequestException("Statuses must be: Delivered, Canceled");
        }

        return Ok(await _orderService.GetMyCourierOrders(userId, status ?? allowedStatuses, number, page, pageSize, sort));
    }

    /// <summary>
    /// [Courier] Get information about specific order.
    /// </summary>
    /// <remarks>
    /// User see his order information with resource based access.
    /// </remarks>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("order/courier/{orderId}")]
    public async Task<ActionResult<OrderFullDto>> GetCourierOrder([FromRoute] Guid orderId) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        if (!await _permissionCheckerService.IsCourierHasAccessToOrder(userId, orderId)) {
            throw new ForbiddenException("Courier has no access to this order");
        }
        // Todo: add validation for transitions
        return Ok(await _orderService.GetOrder(orderId));
    }

    /// <summary>
    /// [Courier] Change order status.
    /// </summary>
    /// <remarks>
    /// Transactions: Packaged_AssignedForCourier, AssignedForCourier_Delivery, Delivery_Delivered, Delivery_Canceled<br/>
    /// </remarks>
    /// <param name="orderId"></param>
    /// <param name="status">Status to set</param>
    /// <returns></returns>
    [HttpPut]
    [Route("order/courier/{orderId}/status")]
    public async Task<ActionResult> ChangeOrderStatusCourier([FromRoute] Guid orderId, [FromBody] OrderChangeStatusDto status) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        if (!await _permissionCheckerService.IsCourierHasAccessToOrder(userId, orderId)) {
            throw new ForbiddenException("Courier has no access to this order");
        }
        
        await _orderService.SetOrderStatus(orderId, status.Status);
        return Ok();
    }

    /// <summary>
    /// [Cook] Get cooks`s orders for work with them.
    /// </summary>
    /// <remarks>
    /// Cook see his orders with statuses: Created.
    /// </remarks>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/cook/available")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCookAvailableOrders(
        [FromQuery] [Optional] List<OrderStatus>? status,
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        var allowedStatuses = new List<OrderStatus>() { OrderStatus.Created };
        if (status != null && !status.All(x => allowedStatuses.Contains(x))) {
            throw new BadRequestException("Statuses must be: Created");
        }

        return Ok(await _orderService.GetMyCookOrders(userId, status ?? allowedStatuses, number, page, pageSize, sort));
    }
    
    /// <summary>
    /// [Cook] Get cooks`s orders for work with them.
    /// </summary>
    /// <remarks>
    /// Cook see his orders with statuses: Kitchen, Packaged.
    /// </remarks>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/cook/current")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCookCurrentOrders(
        [FromQuery] [Optional] List<OrderStatus>? status,
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        var allowedStatuses = new List<OrderStatus>() { OrderStatus.Kitchen, OrderStatus.Packaged  };
        if (status != null && !status.All(x => allowedStatuses.Contains(x))) {
            throw new BadRequestException("Statuses must be: Kitchen, Packaged");
        }

        return Ok(await _orderService.GetMyCookOrders(userId, status ?? allowedStatuses, number, page, pageSize, sort));
    }

    /// <summary>
    /// [Cook] Get cook`s orders history.
    /// </summary>
    /// <remarks>
    /// Cook see his orders with statuses: AssignedForCourier, Delivery, Delivered, Canceled.
    /// </remarks>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/cook/history")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCookOrdersHistory(
        [FromQuery] [Optional] List<OrderStatus>? status, [FromQuery] [Optional] String? number, 
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        var allowedStatuses = new List<OrderStatus>() {
            OrderStatus.AssignedForCourier, OrderStatus.Delivery, OrderStatus.Delivered, OrderStatus.Canceled
        };
        if (status != null && !status.All(x => allowedStatuses.Contains(x))) {
            throw new BadRequestException("Statuses must be: AssignedForCourier, Delivery, Delivered, Canceled");
        }

        return Ok(await _orderService.GetMyCookOrders(userId, status ?? allowedStatuses, number, page, pageSize, sort));
    }

    /// <summary>
    /// [Cook] Get information about specific order.
    /// </summary>
    /// <remarks>
    /// User see his order information with resource based access.
    /// </remarks>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("order/cook/{orderId}")]
    public async Task<ActionResult<OrderFullDto>> GetCookOrder([FromRoute] Guid orderId) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        if (!await _permissionCheckerService.IsCookHasAccessToOrder(userId, orderId)) {
            throw new ForbiddenException("Cook has no access to this order");
        }
        
        return Ok(await _orderService.GetOrder(orderId));
    }

    /// <summary>
    /// [Cook] Change order status.
    /// </summary>
    /// <remarks>
    /// Transactions: Created_Kitchen, Kitchen_Packaged
    /// </remarks>
    /// <param name="orderId"></param>
    /// <param name="status">Status to set</param>
    /// <returns></returns>
    [HttpPut]
    [Route("order/cook/{orderId}/status")]
    public async Task<ActionResult> ChangeOrderStatusCook([FromRoute] Guid orderId, [FromBody] OrderChangeStatusDto status) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }
        // Todo: add validation for transitions
        if (!await _permissionCheckerService.IsCookHasAccessToOrder(userId, orderId)) {
            throw new ForbiddenException("Cook has no access to this order");
        }
        
        await _orderService.SetOrderStatus(orderId, status.Status);
        return Ok();
    }

    /// <summary>
    /// [Customer] Get customer`s current orders.
    /// </summary>
    /// <remarks>
    /// Customer see his orders with statuses: Created, Kitchen, Packaged, AssignedForCourier, Delivery.
    /// </remarks>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number" example="ORD-8800553535-0001">Order number for search</param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/customer/current")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCustomerCurrentOrders(
        [FromQuery] [Optional] List<OrderStatus>? status,
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        var allowedStatuses = new List<OrderStatus>() {
            OrderStatus.Created, OrderStatus.Kitchen, OrderStatus.Packaged, OrderStatus.AssignedForCourier, OrderStatus.Delivery
        };
        if (status != null && !status.All(x => allowedStatuses.Contains(x))) {
            throw new BadRequestException(
                "Statuses must be: Created, Kitchen, Packaged, AssignedForCourier, Delivery");
        }

        return Ok(await _orderService.GetMyCourierOrders(userId, status ?? allowedStatuses, number, page, pageSize, sort));
    }

    /// <summary>
    /// [Customer] Get customer`s orders history.
    /// </summary>
    /// <remarks>
    /// Customer see his orders with statuses: Delivered, Canceled.
    /// </remarks>
    /// <param name="status">Order statuses for filter</param>
    /// <param name="number">Order number for search</param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sort">Sort type</param>
    /// <returns></returns>
    [HttpGet]
    [Route("orders/customer/history")]
    public async Task<ActionResult<Pagination<OrderShortDto>>> GetCustomerOrdersHistory(
        [FromQuery] [Optional] List<OrderStatus>? status,
        [FromQuery] [Optional] String? number, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] OrderSort sort = OrderSort.CreationDesc) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        var allowedStatuses = new List<OrderStatus>() { OrderStatus.Delivered, OrderStatus.Canceled };
        if (status != null && !status.All(x => allowedStatuses.Contains(x))) {
            throw new BadRequestException("Statuses must be: Delivered, Canceled");
        }

        return Ok(await _orderService.GetMyCourierOrders(userId, status ?? allowedStatuses, number, page, pageSize, sort));
    }

    /// <summary>
    /// [Customer] Get information about specific order.
    /// </summary>
    /// <remarks>
    /// User see his order information with resource based access.
    /// </remarks>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("order/customer/{orderId}")]
    public async Task<ActionResult<OrderFullDto>> GetCustomerOrder([FromRoute] Guid orderId) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }

        if (!await _permissionCheckerService.IsCustomerHasAccessToOrder(userId, orderId)) {
            throw new ForbiddenException("Customer has no access to this order");
        }
        
        return Ok(_orderService.GetOrder(orderId));
    }

    /// <summary>
    /// [Customer] Change order status.
    /// </summary>
    /// <remarks>
    /// Transactions: Created_Canceled
    /// </remarks>
    /// <param name="orderId"></param>
    /// <param name="status">Status to set</param>
    /// <returns></returns>
    [HttpPut]
    [Route("order/customer/{orderId}/status")]
    public async Task<ActionResult> ChangeOrderStatusCustomer([FromRoute] Guid orderId, [FromBody] OrderChangeStatusDto status) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }
        // Todo: add validation for transitions
        if (!await _permissionCheckerService.IsCookHasAccessToOrder(userId, orderId)) {
            throw new ForbiddenException("Cook has no access to this order");
        }
        
        await _orderService.SetOrderStatus(orderId, status.Status);
        return Ok();
    }

    /// <summary>
    /// [Manager] Get information about specific order.
    /// </summary>
    /// <remarks>
    /// User see his order information with resource based access.
    /// </remarks>
    /// <param name="orderId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("order/{orderId}")]
    public async Task<ActionResult<OrderFullDto>> GetOrder([FromRoute] Guid orderId) {
        return Ok(await _orderService.GetOrder(orderId));
    }

    /// <summary>
    /// [Manager] Change order status.
    /// </summary>
    /// <remarks>
    /// Transactions:  Created_Kitchen, Kitchen_Packaged, Packaged_AssignedForCourier, AssignedForCourier_Delivery,
    /// Delivery_Delivered, Any_Canceled<br/>
    /// </remarks>
    /// <param name="orderId"></param>
    /// <param name="status">Status to set</param>
    /// <returns></returns>
    [HttpPut]
    [Route("order/{orderId}/status")]
    public async Task<ActionResult> ChangeOrderStatus([FromRoute] Guid orderId, [FromBody] OrderChangeStatusDto status) {
        // Todo: add validation for transitions
        await _orderService.SetOrderStatus(orderId, status.Status);
        return Ok();
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
    public async Task<ActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto) {
        if (User.Identity == null || Guid.TryParse(User.Identity.Name, out Guid userId) == false) {
            throw new UnauthorizedException("User is not authorized");
        }
        
        await _orderService.CreateOrder(userId, orderCreateDto);
        return Ok();
    }

    /// <summary>
    /// [Customer] Repeats Delivered order, by adding all dishes of them to cart.
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="replaceCart">If true, clear cart and add dishes. Else only try to add dishes</param>
    /// <returns></returns>
    [HttpPost]
    [Route("order/{orderId}/repeat")]
    public async Task<ActionResult> RepeatOrder([FromRoute] Guid orderId, Boolean? replaceCart = false) {
        await _orderService.RepeatOrder(orderId, replaceCart ?? false);
        return Ok();
    }
}