using AutoMapper;
using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.BackendAPI.DAL;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.AdminPanel.BL.Services; 

public class AdminPanelRestaurantService : IAdminPanelRestaurantService {
    private readonly AuthDbContext _authDbContext;
    private readonly BackendDbContext _backendDbContext;
    private readonly IMapper _mapper;

    public AdminPanelRestaurantService(AuthDbContext authDbContext, BackendDbContext backendDbContext, IMapper mapper) {
        _authDbContext = authDbContext;
        _backendDbContext = backendDbContext;
        _mapper = mapper;
    }
    

    /// <summary>
    /// Get all unarchived restaurants
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public Pagination<RestaurantShortDto> GetAllUnarchivedRestaurants(String? name, int page, int pageSize = 10) {
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
            .OrderBy(x=>x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var mapped = _mapper.Map<List<RestaurantShortDto>>(raw);
        return new Pagination<RestaurantShortDto>(mapped, page, pageSize, pages);
    }

    public RestaurantFullDto GetRestaurant(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants?.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var mapped = _mapper.Map<RestaurantFullDto>(restaurant);
        return mapped;
    }

    public List<AccountProfileFullDto> GetRestaurantManagers(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants?.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        var managers = _authDbContext.Users?.Where(x => restaurant.Managers.Contains(x.Id)).ToList();
        var mapped = _mapper.Map<List<AccountProfileFullDto>>(managers);
        return mapped;
    }

    public List<AccountProfileFullDto> GetRestaurantCooks(Guid restaurantId) {
        throw new NotImplementedException();
    }

    public RestaurantFullDto CreateRestaurant(RestaurantCreateDto restaurantCreateDto) {
        throw new NotImplementedException();
    }

    public RestaurantFullDto UpdateRestaurant(Guid restaurantId, RestaurantUpdateDto restaurantUpdateDto) {
        throw new NotImplementedException();
    }

    public RestaurantFullDto ArchiveRestaurant(Guid restaurantId) {
        throw new NotImplementedException();
    }

    public RestaurantFullDto UnarchiveRestaurant(Guid restaurantId) {
        throw new NotImplementedException();
    }

    public List<RestaurantShortDto> GetArchivedRestaurants() {
        throw new NotImplementedException();
    }

    public Pagination<OrderShortDto> GetRestaurantOrders(Guid restaurantId, OrderSort sort, List<OrderStatus>? status, string? number, int page = 1) {
        throw new NotImplementedException();
    }

    public async Task AddManagerToRestaurant(Guid restaurantId, string email) {
        var restaurant = _backendDbContext.Restaurants?.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var user = _authDbContext.Users?.Include(x=>x.Manager).FirstOrDefault(x => x.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        
        user.Manager ??= new Manager() {
            Id = Guid.NewGuid()
        };
        
        restaurant.Managers ??= new List<Guid>();
        
        if (restaurant.Managers.Contains(user.Id)) {
            throw new MethodNotAllowedException("User already is a manager of this restaurant");
        }
        
        restaurant.Managers ??= new List<Guid>();
        restaurant.Managers.Add(user.Id);
        await _backendDbContext.SaveChangesAsync();
        await _authDbContext.SaveChangesAsync();
    }

    public async Task RemoveManagerFromRestaurant(Guid restaurantId, string email) {
        var restaurant = _backendDbContext.Restaurants?.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var user = _authDbContext.Users?.AsNoTracking().Include(x=>x.Manager).FirstOrDefault(x => x.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        
        restaurant.Managers ??= new List<Guid>();
        
        if (!restaurant.Managers.Contains(user.Id)) {
            throw new MethodNotAllowedException("User is not a manager of this restaurant");
        }
        
        restaurant.Managers.Remove(user.Id);
        await _backendDbContext.SaveChangesAsync();
    }

    public Task AddCookToRestaurant(Guid restaurantId, string email) {
        throw new NotImplementedException();
    }
}