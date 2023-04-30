using AutoMapper;
using Delivery.AuthAPI.DAL;
using Delivery.AuthAPI.DAL.Entities;
using Delivery.BackendAPI.DAL;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Delivery.AdminPanel.BL.Services;

public class AdminPanelRestaurantService : IAdminPanelRestaurantService {
    private readonly AuthDbContext _authDbContext;
    private readonly BackendDbContext _backendDbContext;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AdminPanelRestaurantService(AuthDbContext authDbContext, BackendDbContext backendDbContext, IMapper mapper,
        UserManager<User> userManager) {
        _authDbContext = authDbContext;
        _backendDbContext = backendDbContext;
        _mapper = mapper;
        _userManager = userManager;
    }


    /// <summary>
    /// Get all unarchived restaurants
    /// </summary>
    /// <param name="name"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="isArchived"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public Pagination<RestaurantShortDto> GetAllRestaurants(String? name, int page, int pageSize = 10,
        bool? isArchived = null, RestaurantSort? sort = RestaurantSort.NameAsc) {
        var allCount = _backendDbContext.Restaurants
            .Where(x => (isArchived == null || x.IsArchived == isArchived))
            .Count(x => name == null ? true : x.Name.Contains(name));
        if (allCount == 0) {
            return new Pagination<RestaurantShortDto>(new List<RestaurantShortDto>(), page, pageSize, 0);
        }

        // Calculate pages amount
        var pages = (int)Math.Ceiling((double)allCount / pageSize);
        if (page > pages) {
            throw new NotFoundException("Restaurants not found");
        }

        // Get restaurants
        if (sort == RestaurantSort.NameAsc) {
            var raw = _backendDbContext.Restaurants?
                .Where(x => (isArchived == null || x.IsArchived == isArchived))
                .Where(x => name == null ? true : x.Name.Contains(name))
                .OrderBy(x => x.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var mapped = _mapper.Map<List<RestaurantShortDto>>(raw);
            return new Pagination<RestaurantShortDto>(mapped, page, pageSize, pages);
        }
        else {
            var raw = _backendDbContext.Restaurants?
                .Where(x => (isArchived == null || x.IsArchived == isArchived))
                .Where(x => name == null ? true : x.Name.Contains(name))
                .OrderByDescending(x => x.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var mapped = _mapper.Map<List<RestaurantShortDto>>(raw);
            return new Pagination<RestaurantShortDto>(mapped, page, pageSize, pages);
        }
    }

    public RestaurantFullDto GetRestaurant(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var mapped = _mapper.Map<RestaurantFullDto>(restaurant);
        return mapped;
    }

    public List<AccountProfileFullDto> GetRestaurantManagers(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var managers = _authDbContext.Users.Where(x => restaurant.Managers.Contains(x.Id)).ToList();
        var mapped = _mapper.Map<List<AccountProfileFullDto>>(managers);
        return mapped;
    }

    public List<AccountProfileFullDto> GetRestaurantCooks(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var cooks = _authDbContext.Users.Where(x => restaurant.Cooks.Contains(x.Id)).ToList();
        var mapped = _mapper.Map<List<AccountProfileFullDto>>(cooks);
        return mapped;
    }

    public async Task CreateRestaurant(RestaurantCreateDto restaurantCreateDto) {
        var restaurant = _mapper.Map<Restaurant>(restaurantCreateDto);
        restaurant.CreatedAt = DateTime.UtcNow;
        restaurant.UpdatedAt = DateTime.UtcNow;
        _backendDbContext.Restaurants.Add(restaurant);
        await _backendDbContext.SaveChangesAsync();
    }

    public async Task UpdateRestaurant(Guid restaurantId, RestaurantEditDto restaurantEditDto) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        restaurant.Name = restaurantEditDto.Name;
        restaurant.Description = restaurantEditDto.Description;
        restaurant.Address = restaurantEditDto.Address;
        restaurant.BigImage = restaurantEditDto.BigImage;
        restaurant.SmallImage = restaurantEditDto.SmallImage;
        restaurant.UpdatedAt = DateTime.UtcNow;

        await _backendDbContext.SaveChangesAsync();
    }

    public async Task ArchiveRestaurant(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        restaurant.IsArchived = true;

        await _backendDbContext.SaveChangesAsync();
    }

    public async Task UnarchiveRestaurant(Guid restaurantId) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        restaurant.IsArchived = false;

        await _backendDbContext.SaveChangesAsync();
    }

    public async Task AddManagerToRestaurant(Guid restaurantId, string email) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var user = _authDbContext.Users.Include(x => x.Manager).FirstOrDefault(x => x.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (!await _userManager.IsInRoleAsync(user, RoleType.Manager.ToString())){
            await _userManager.AddToRoleAsync(user, RoleType.Manager.ToString());
        }

        if (restaurant.Managers.Contains(user.Id)) {
            throw new MethodNotAllowedException("User already is a manager of this restaurant");
        }

        restaurant.Managers.Add(user.Id);
        await _backendDbContext.SaveChangesAsync();
        await _authDbContext.SaveChangesAsync();
    }

    public async Task RemoveManagerFromRestaurant(Guid restaurantId, string email) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var user = _authDbContext.Users.Include(x => x.Manager).FirstOrDefault(x => x.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }
        
        if (_backendDbContext.Restaurants.Count(x => x.Managers.Contains(user.Id)) == 1){
            await _userManager.RemoveFromRoleAsync(user, RoleType.Manager.ToString());
        }

        if (!restaurant.Managers.Contains(user.Id)) {
            throw new MethodNotAllowedException("User is not a manager of this restaurant");
        }

        restaurant.Managers.Remove(user.Id);
        await _backendDbContext.SaveChangesAsync();
    }

    public async Task AddCookToRestaurant(Guid restaurantId, string email) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var user = _authDbContext.Users.Include(x => x.Cook).FirstOrDefault(x => x.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (!await _userManager.IsInRoleAsync(user, RoleType.Cook.ToString())){
            await _userManager.AddToRoleAsync(user, RoleType.Cook.ToString());
        }

        if (restaurant.Cooks.Contains(user.Id)) {
            throw new MethodNotAllowedException("User already is a cook of this restaurant");
        }

        restaurant.Cooks.Add(user.Id);
        await _backendDbContext.SaveChangesAsync();
        await _authDbContext.SaveChangesAsync();
    }

    public async Task RemoveCookFromRestaurant(Guid restaurantId, string email) {
        var restaurant = _backendDbContext.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var user = _authDbContext.Users.Include(x => x.Cook).FirstOrDefault(x => x.Email == email);
        if (user == null) {
            throw new NotFoundException("User not found");
        }

        if (_backendDbContext.Restaurants.Count(x => x.Cooks.Contains(user.Id)) == 1){
            await _userManager.RemoveFromRoleAsync(user, RoleType.Cook.ToString());
        }

        if (!restaurant.Cooks.Contains(user.Id)) {
            throw new MethodNotAllowedException("User is not a manager of this restaurant");
        }

        restaurant.Cooks.Remove(user.Id);
        await _backendDbContext.SaveChangesAsync();
    }
}