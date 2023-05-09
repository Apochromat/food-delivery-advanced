using AutoMapper;
using Delivery.BackendAPI.DAL;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.BackendAPI.DAL.Extensions;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Extensions;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BackendAPI.BL.Services;

/// <summary>
/// Order service
/// </summary>
public class OrderService : IOrderService {
    private readonly BackendDbContext _backendDbContext;
    private readonly ICartService _cartService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    /// <param name="mapper"></param>
    /// <param name="cartService"></param>
    public OrderService(BackendDbContext backendDbContext, IMapper mapper, ICartService cartService) {
        _backendDbContext = backendDbContext;
        _mapper = mapper;
        _cartService = cartService;
    }

    /// <inheritdoc />
    public async Task<Pagination<OrderShortDto>> GetAllOrders(List<OrderStatus>? status = null, string? number = null,
        int page = 1, int pageSize = 10, OrderSort sort = OrderSort.CreationDesc) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }

        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }

        var allCount = await _backendDbContext.Orders.CountAsync(x =>
            (number == null || x.Number.Contains(number))
            && (status == null || status.Contains(x.Status)));
        if (allCount == 0) {
            throw new NotFoundException("Orders not found");
        }

        // Calculate pages amount
        var pages = (int)Math.Ceiling((double)allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }

        // Get orders
        var raw = await _backendDbContext.Orders.Where(x =>
                (number == null || x.Number.Contains(number))
                && (status == null || status.Contains(x.Status)))
            .OrderByOrderSort(sort)
            .TakePage(page, pageSize)
            .ToListAsync();

        var mapped = _mapper.Map<List<OrderShortDto>>(raw);
        return new Pagination<OrderShortDto>(mapped, page, pageSize, pages);
    }

    /// <inheritdoc />
    public async Task<Pagination<OrderShortDto>> GetMyCustomerOrders(Guid customerId, List<OrderStatus>? status = null,
        string? number = null, int page = 1, int pageSize = 10, OrderSort sort = OrderSort.CreationDesc) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }

        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }

        var allCount = await _backendDbContext.Orders.CountAsync(x =>
            (number == null || x.Number.Contains(number))
            && (status == null || status.Contains(x.Status))
            && x.CustomerId == customerId);
        if (allCount == 0) {
            throw new NotFoundException("Orders not found");
        }

        // Calculate pages amount
        var pages = (int)Math.Ceiling((double)allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }

        // Get orders
        var raw = await _backendDbContext.Orders.Where(x =>
                (number == null || x.Number.Contains(number))
                && (status == null || status.Contains(x.Status))
                && x.CustomerId == customerId)
            .OrderByOrderSort(sort)
            .TakePage(page, pageSize)
            .ToListAsync();

        var mapped = _mapper.Map<List<OrderShortDto>>(raw);
        return new Pagination<OrderShortDto>(mapped, page, pageSize, pages);
    }

    /// <inheritdoc />
    public async Task<Pagination<OrderShortDto>> GetMyCookOrders(Guid cookId, List<OrderStatus>? status, string? number,
        int page = 1, int pageSize = 10, OrderSort sort = OrderSort.CreationDesc) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }

        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }

        var allCount = await _backendDbContext.Orders.CountAsync(x =>
            (number == null || x.Number.Contains(number))
            && (status == null || status.Contains(x.Status))
            && x.CookId == cookId);
        if (allCount == 0) {
            throw new NotFoundException("Orders not found");
        }

        // Calculate pages amount
        var pages = (int)Math.Ceiling((double)allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }

        // Get orders
        var raw = await _backendDbContext.Orders.Where(x =>
                (number == null || x.Number.Contains(number))
                && (status == null || status.Contains(x.Status))
                && x.CookId == cookId)
            .OrderByOrderSort(sort)
            .TakePage(page, pageSize)
            .ToListAsync();

        var mapped = _mapper.Map<List<OrderShortDto>>(raw);
        return new Pagination<OrderShortDto>(mapped, page, pageSize, pages);
    }

    /// <inheritdoc />
    public async Task<Pagination<OrderShortDto>> GetMyCourierOrders(Guid courierId, List<OrderStatus>? status,
        string? number, int pageSize = 10, int page = 1, OrderSort sort = OrderSort.CreationDesc) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }

        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }

        var allCount = await _backendDbContext.Orders.CountAsync(x =>
            (number == null || x.Number.Contains(number))
            && (status == null || status.Contains(x.Status))
            && x.CourierId == courierId);
        if (allCount == 0) {
            throw new NotFoundException("Orders not found");
        }

        // Calculate pages amount
        var pages = (int)Math.Ceiling((double)allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }

        // Get orders
        var raw = await _backendDbContext.Orders.Where(x =>
                (number == null || x.Number.Contains(number))
                && (status == null || status.Contains(x.Status))
                && x.CourierId == courierId)
            .OrderByOrderSort(sort)
            .TakePage(page, pageSize)
            .ToListAsync();

        var mapped = _mapper.Map<List<OrderShortDto>>(raw);
        return new Pagination<OrderShortDto>(mapped, page, pageSize, pages);
    }

    /// <inheritdoc />
    public async Task<OrderFullDto> GetOrder(Guid orderId) {
        var order = await _backendDbContext.Orders.Include(x=>x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == orderId);
        if (order == null) {
            throw new NotFoundException("Order not found");
        }

        return _mapper.Map<OrderFullDto>(order);
    }

    /// <inheritdoc />
    public async Task CreateOrder(Guid customerId, OrderCreateDto orderCreateDto) {
        var dishesInCart = await _backendDbContext.DishesInCart
            .Include(x => x.Dish)
            .Include(x => x.Restaurant)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
        
        if (dishesInCart.Count == 0) {
            throw new BadRequestException("Cart is empty");
        }

        var orderDishes = dishesInCart.Select(x => new OrderDish() {
            Id = new Guid(),
            Dish = x.Dish,
            Amount = x.Amount,
            ArchivedDishPrice = x.Dish.Price,
            ArchivedDishName = x.Dish.Name
        }).ToList();
        
        var order = new Order() {
            Id = new Guid(),
            CustomerId = customerId,
            Number = "ORD-" + Guid.NewGuid().ToString().Substring(0, 8),
            Status = OrderStatus.Created,
            OrderTime = DateTime.UtcNow,
            DeliveryTime = orderCreateDto.DeliveryTime,
            Address = orderCreateDto.Address,
            Comment = orderCreateDto.Comment,
            CookId = null,
            CourierId = null,
            Dishes = orderDishes,
            Restaurant = dishesInCart.First().Restaurant
        };
        
        await _cartService.ClearCart(order.CustomerId);
        await _backendDbContext.Orders.AddAsync(order);
        await _backendDbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task SetOrderStatus(Guid orderId, OrderStatus status) {
        var order = await _backendDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        if (order == null) {
            throw new NotFoundException("Order not found");
        }
        
        // TODO: check if status is valid, add state machine
        order.Status = status;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task SetOrderCourier(Guid orderId, Guid courierId) {
        var order = await _backendDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        if (order == null) {
            throw new NotFoundException("Order not found");
        }
        
        // TODO: check if status is valid, add state machine
        order.CourierId = courierId;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task SerOrderCook(Guid orderId, Guid cookId) {
        var order = await _backendDbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
        if (order == null) {
            throw new NotFoundException("Order not found");
        }
        
        // TODO: check if status is valid, add state machine
        order.CookId = cookId;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RepeatOrder(Guid orderId, bool force = false) {
        var order = await _backendDbContext.Orders.Include(x=>x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == orderId);
        if (order == null) {
            throw new NotFoundException("Order not found");
        }
        
        var cart = await _cartService.GetCart(order.CustomerId);
        if (cart.Dishes.Count != 0 && force == false) {
            throw new BadRequestException("Cart is not empty. Send force=true to override");
        }

        await _cartService.ClearCart(order.CustomerId);
        foreach (var orderDish in order.Dishes) {
            await _cartService.AddDishToCart(order.CustomerId, orderDish.Dish.Id, orderDish.Amount);
        }
        
    }
}