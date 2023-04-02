using Delivery.BackendAPI.DAL;
using Delivery.Common.DTO;
using Delivery.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Delivery.BackendAPI.BL.Services; 

public class RestaurantService : IRestaurantService {
    private readonly ILogger<RestaurantService> _logger;
    private readonly BackendDbContext _backendDbContext;

    public RestaurantService(BackendDbContext backendDbContext, ILogger<RestaurantService> logger) {
        _backendDbContext = backendDbContext;
        _logger = logger;
    }
    
    public List<RestaurantShortDto> GetAllRestaurants() {
        var raw = _backendDbContext.Restaurants?.ToList();
        if (raw == null) {
            return new List<RestaurantShortDto>();
        }
        var result = raw.Select(x => new RestaurantShortDto {
            Id = x.Id,
            Name = x.Name
        }).ToList();
        return result;
    }
}