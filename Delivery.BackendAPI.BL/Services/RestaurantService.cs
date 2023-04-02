using AutoMapper;
using Delivery.BackendAPI.DAL;
using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Delivery.BackendAPI.BL.Services; 

public class RestaurantService : IRestaurantService {
    private readonly ILogger<RestaurantService> _logger;
    private readonly IMapper _mapper;
    private readonly BackendDbContext _backendDbContext;

    public RestaurantService(BackendDbContext backendDbContext, ILogger<RestaurantService> logger, IMapper mapper) {
        _backendDbContext = backendDbContext;
        _logger = logger;
        _mapper = mapper;
    }
    
    public Pagination<RestaurantShortDto> GetAllRestaurants(String name, int page, int pageSize = 10) {
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

    public RestaurantFullDto GetRestaurant(Guid restaurantId) {
        throw new NotImplementedException();
    }

    public RestaurantFullDto CreateRestaurant(RestaurantCreateDto restaurantCreateDto) {
        throw new NotImplementedException();
    }

    public RestaurantFullDto UpdateRestaurant(Guid restaurantId, RestaurantUpdateDto restaurantUpdateDto) {
        throw new NotImplementedException();
    }

    public void DeleteRestaurant(Guid restaurantId) {
        throw new NotImplementedException();
    }
}