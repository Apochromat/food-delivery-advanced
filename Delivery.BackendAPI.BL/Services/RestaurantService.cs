using AutoMapper;
using Delivery.BackendAPI.DAL;
using Delivery.BackendAPI.DAL.Extensions;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;

namespace Delivery.BackendAPI.BL.Services; 

/// <summary>
/// Service for restaurant operations
/// </summary>
public class RestaurantService : IRestaurantService {
    private readonly IMapper _mapper;
    private readonly BackendDbContext _backendDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    /// <param name="mapper"></param>
    public RestaurantService(BackendDbContext backendDbContext, IMapper mapper) {
        _backendDbContext = backendDbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Get list of all unarchived restaurants
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="name"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public Task<Pagination<RestaurantShortDto>> GetAllUnarchivedRestaurants(int page, int pageSize = 10, RestaurantSort sort = RestaurantSort.NameAsc, String? name = null) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }
        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }
        
        var allCount = _backendDbContext.Restaurants
            .Count(x => x.IsArchived == false 
                        && (name == null || x.Name.Contains(name)));
        if (allCount == 0) {
            throw new NotFoundException("Restaurants not found");
        }
        
        // Calculate pages amount
        var pages = (int) Math.Ceiling((double) allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }
        
        // Get restaurants
        var raw = _backendDbContext.Restaurants
            .Where(x => x.IsArchived == false 
                        && (name == null || x.Name.Contains(name)))
            .OrderByRestaurantSort(sort)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var mapped = _mapper.Map<List<RestaurantShortDto>>(raw);
        return Task.FromResult(new Pagination<RestaurantShortDto>(mapped, page, pageSize, pages));
    }

    /// <summary>
    /// Get full restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<RestaurantFullDto> GetRestaurant(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var mapped = _mapper.Map<RestaurantFullDto>(restaurant);
        return Task.FromResult(mapped);
    }

    /// <summary>
    /// Update existing restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantEditDto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task EditRestaurant(Guid restaurantId, RestaurantEditDto restaurantEditDto) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        restaurant.Name = restaurantEditDto.Name;
        restaurant.Description = restaurantEditDto.Description;
        restaurant.Address = restaurantEditDto.Address;
        restaurant.SmallImage = restaurantEditDto.SmallImage;
        restaurant.BigImage = restaurantEditDto.BigImage;
        
        return _backendDbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Get list of all restaurant orders
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="sort"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Pagination<OrderShortDto>> GetRestaurantOrders(Guid restaurantId, OrderSort sort, 
        List<OrderStatus>? status, string? number, int page = 1, int pageSize = 10) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }
        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }
        
        var allCount = _backendDbContext.Orders
            .Count(x => x.Restaurant.Id == restaurantId 
                        && (status == null || status.Contains(x.Status)) 
                        && (number == null || x.Number.Contains(number)));
        if (allCount == 0) {
            throw new NotFoundException("Orders not found");
        }
        
        // Calculate pages amount
        var pages = (int) Math.Ceiling((double) allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }
        
        // Get orders
        var raw = _backendDbContext.Orders
            .Where(x => x.Restaurant.Id == restaurantId 
                        && (status == null || status.Contains(x.Status)) 
                        && (number == null || x.Number.Contains(number)))
            .OrderByOrderSort(sort)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var mapped = _mapper.Map<List<OrderShortDto>>(raw);
        return Task.FromResult(new Pagination<OrderShortDto>(mapped, page, pageSize, pages));
    }

    /// <summary>
    /// Get list of restaurant orders for cooks
    /// </summary>
    /// <remarks>
    /// Cook see orders in his restaurant with statuses: Created
    /// </remarks>
    /// <param name="restaurantId"></param>
    /// <param name="sort"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="BadRequestException"></exception>
    public Task<Pagination<OrderShortDto>> GetCookRestaurantOrders(Guid restaurantId, OrderSort sort, string? number, 
        int page = 1, int pageSize = 10) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }
        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }
        
        var allCount = _backendDbContext.Orders
            .Count(x => x.Restaurant.Id == restaurantId 
                        && (number == null || x.Number.Contains(number)));
        if (allCount == 0) {
            throw new NotFoundException("Orders not found");
        }
        
        // Calculate pages amount
        var pages = (int) Math.Ceiling((double) allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }
        
        // Get orders
        var raw = _backendDbContext.Orders
            .Where(x => x.Restaurant.Id == restaurantId
                        && (number == null || x.Number.Contains(number))
                        && x.Status == OrderStatus.Created)
            .OrderByOrderSort(sort)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var mapped = _mapper.Map<List<OrderShortDto>>(raw);
        return Task.FromResult(new Pagination<OrderShortDto>(mapped, page, pageSize, pages));
    }
}