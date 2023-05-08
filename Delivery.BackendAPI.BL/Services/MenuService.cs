using AutoMapper;
using Delivery.BackendAPI.DAL;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BackendAPI.BL.Services; 

/// <summary>
/// Menu service 
/// </summary>
public class MenuService : IMenuService {
    private readonly BackendDbContext _backendDbContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    /// <param name="mapper"></param>
    public MenuService(BackendDbContext backendDbContext, IMapper mapper) {
        _backendDbContext = backendDbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Create menu
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="menuCreateDto"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task CreateRestaurantMenu(Guid restaurantId, MenuCreateDto menuCreateDto) {
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId);
        if (restaurant== null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var menu = new Menu() {
            Id = new Guid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            RestaurantId = restaurant.Id,
            Dishes = new List<Dish>(),
            IsArchived = false,
            IsDefault = false,
            Name = menuCreateDto.Name
        };
        
        restaurant.Menus.Add(menu);
        _backendDbContext.Menus.Add(menu);
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get restaurant menus
    /// </summary>
    /// <param name="name"></param>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<List<MenuShortDto>> GetRestaurantMenus(string? name, Guid restaurantId) {
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var menus = await _backendDbContext.Menus
            .Where(x => 
                x.RestaurantId == restaurantId 
                && (name == null || x.Name.Contains(name))
                && !x.IsArchived)
            .ToListAsync();
        
        var mapped = _mapper.Map<List<MenuShortDto>>(menus);
        return mapped;
    }
    
    /// <summary>
    /// Get menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<MenuFullDto> GetMenu(Guid menuId) {
        var menu = await _backendDbContext.Menus
            .Include(x => x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == menuId);
        if (menu == null) {
            throw new NotFoundException("Menu not found");
        }
        
        var mapped = _mapper.Map<MenuFullDto>(menu);
        return mapped;
    }

    /// <summary>
    /// Archive menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task ArchiveMenu(Guid menuId) {
        var menu = await _backendDbContext.Menus
            .FirstOrDefaultAsync(x => x.Id == menuId);
        if (menu == null) {
            throw new NotFoundException("Menu not found");
        }
        if (menu.IsDefault) {
            throw new MethodNotAllowedException("This menu is default and can't be archived");
        }
        
        menu.IsArchived = true;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Unarchive menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task UnarchiveMenu(Guid menuId) {
        var menu = await _backendDbContext.Menus
            .FirstOrDefaultAsync(x => x.Id == menuId);
        if (menu == null) {
            throw new NotFoundException("Menu not found");
        }
        if (menu.IsDefault) {
            throw new MethodNotAllowedException("This menu is default and can't be unarchived");
        }
        
        menu.IsArchived = false;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get archived restaurant menus
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<MenuShortDto>> ArchivedRestaurantMenus(Guid restaurantId) {
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var menus = await _backendDbContext.Menus
            .Where(x => 
                x.RestaurantId == restaurantId 
                && x.IsArchived)
            .ToListAsync();
        var mapped = _mapper.Map<List<MenuShortDto>>(menus);
        return mapped;
    }

    /// <summary>
    /// Add dish to menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task AddDishToMenu(Guid menuId, Guid dishId) {
        var menu = await _backendDbContext.Menus.Include(x => x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == menuId);
        if (menu == null) {
            throw new NotFoundException("Menu not found");
        }
        if (menu.IsDefault) {
            throw new MethodNotAllowedException("This menu is default and can't be edited");
        }
        
        var dish = await _backendDbContext.Dishes
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }
        
        // ToDo: разобраться с лишними null
        menu.Dishes??= new List<Dish>();
        menu.Dishes.Add(dish);
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Remove dish from menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="dishId"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task RemoveDishFromMenu(Guid menuId, Guid dishId) {
        var menu = await _backendDbContext.Menus
            .Include(x => x.Dishes)
            .FirstOrDefaultAsync(x => x.Id == menuId);
        if (menu == null) {
            throw new NotFoundException("Menu not found");
        }
        if (menu.IsDefault) {
            throw new MethodNotAllowedException("This menu is default and can't be edited");
        }
        
        var dish = await _backendDbContext.Dishes
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }
        
        // Todo: разобраться с лишними null
        menu.Dishes??= new List<Dish>();
        menu.Dishes.Remove(dish);
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Edit menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="menuEditDto"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task EditMenu(Guid menuId, MenuEditDto menuEditDto) {
        var menu = await _backendDbContext.Menus
            .FirstOrDefaultAsync(x => x.Id == menuId);
        if (menu == null) {
            throw new NotFoundException("Menu not found");
        }
        if (menu.IsDefault) {
            throw new MethodNotAllowedException("This menu is default and can't be edited");
        }
        
        menu.Name = menuEditDto.Name;
        await _backendDbContext.SaveChangesAsync();
    }
}