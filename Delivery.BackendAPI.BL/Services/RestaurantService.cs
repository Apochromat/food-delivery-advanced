using System.Text;
using System.Text.Json;
using AutoMapper;
using Delivery.BackendAPI.DAL;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Delivery.BackendAPI.BL.Services; 

/// <summary>
/// Service for restaurant operations
/// </summary>
public class RestaurantService : IRestaurantService {
    private readonly ILogger<RestaurantService> _logger;
    private readonly IMapper _mapper;
    private readonly BackendDbContext _backendDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public RestaurantService(BackendDbContext backendDbContext, ILogger<RestaurantService> logger, IMapper mapper) {
        _backendDbContext = backendDbContext;
        _logger = logger;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get list of all unarchived restaurants
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public Pagination<RestaurantShortDto> GetAllUnarchivedRestaurants(String name, int page, int pageSize = 10) {
        var allCount = _backendDbContext.Restaurants?.Where(x => x.IsArchived == false).Count();
        if (allCount == null) {
            throw new NotFoundException("Restaurants not found");
        }
        // Calculate pages amount
        var pages = (int) Math.Ceiling((double) allCount / pageSize);
        if (page > pages) {
            throw new NotFoundException("Restaurants not found");
        }
        // Get restaurants
        var raw = _backendDbContext.Restaurants?
            .Where(x => x.IsArchived == false)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var mapped = _mapper.Map<List<RestaurantShortDto>>(raw);
        return new Pagination<RestaurantShortDto>(mapped, page, pageSize, pages);
    }

    /// <summary>
    /// Get full restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public RestaurantFullDto GetRestaurant(Guid restaurantId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create new restaurant
    /// </summary>
    /// <param name="restaurantCreateDto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public RestaurantFullDto CreateRestaurant(RestaurantCreateDto restaurantCreateDto) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Update existing restaurant info
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="restaurantEditDto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public RestaurantFullDto EditRestaurant(Guid restaurantId, RestaurantEditDto restaurantEditDto) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set restaurant as archived
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public RestaurantFullDto ArchiveRestaurant(Guid restaurantId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set restaurant as unarchived
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public RestaurantFullDto UnarchiveRestaurant(Guid restaurantId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get list of all archived restaurants
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<RestaurantShortDto> GetArchivedRestaurants() {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get list of all restaurant orders
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="sort"></param>
    /// <param name="status"></param>
    /// <param name="number"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Pagination<OrderShortDto> GetRestaurantOrders(Guid restaurantId, OrderSort sort, List<OrderStatus>? status, string? number, int page = 1) {
        throw new NotImplementedException();
    }
}