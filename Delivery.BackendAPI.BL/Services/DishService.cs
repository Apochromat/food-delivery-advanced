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
/// Service for dish management
/// </summary>
public class DishService : IDishService {
    private readonly BackendDbContext _backendDbContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    /// <param name="mapper"></param>
    public DishService(BackendDbContext backendDbContext, IMapper mapper) {
        _backendDbContext = backendDbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Create dish
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="dishCreateDto"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task CreateDish(Guid restaurantId, DishCreateDto dishCreateDto) {
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }

        var menu = await _backendDbContext.Menus
            .FirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.IsDefault);

        if (menu == null) {
            menu = new Menu() {
                Id = new Guid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RestaurantId = restaurant.Id,
                Dishes = new List<Dish>(),
                IsArchived = false,
                IsDefault = true,
                Name = "Default"
            };
            restaurant.Menus.Add(menu);
            _backendDbContext.Menus.Add(menu);
        }

        var dish = new Dish() {
            Id = new Guid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Menus = new List<Menu?>() { menu },
            Name = dishCreateDto.Name,
            Description = dishCreateDto.Description,
            Price = dishCreateDto.Price,
            IsVegetarian = dishCreateDto.IsVegetarian,
            IsArchived = false,
            DishCategories = dishCreateDto.DishCategories
        };
        menu.Dishes?.Add(dish);
        _backendDbContext.Dishes.Add(dish);
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get list of all unarchived dishes
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="menus"></param>
    /// <param name="categories"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="name"></param>
    /// <param name="isVegetarian"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    /// <exception cref="BadRequestException"></exception>
    /// <exception cref="NotFoundException"></exception>
    public Task<Pagination<DishShortDto>> GetAllUnarchivedDishes(Guid restaurantId, List<Guid>? menus,
        List<DishCategory>? categories, int page, int pageSize = 10, string? name = null,
        bool isVegetarian = false, DishSort sort = DishSort.NameAsc) {
        if (page < 1) {
            throw new BadRequestException("Page number must be greater than 0");
        }

        if (pageSize < 1) {
            throw new BadRequestException("Page size must be greater than 0");
        }

        var allCount = _backendDbContext.Dishes
            .FilterDishes(restaurantId, menus, categories, name, false, isVegetarian)
            .Count();
        if (allCount == 0) {
            throw new NotFoundException("Dishes not found");
        }

        // Calculate pages amount
        var pages = (int)Math.Ceiling((double)allCount / pageSize);
        if (page > pages) {
            throw new BadRequestException("Page number is too big");
        }

        // Get dishes
        var raw = _backendDbContext.Dishes
            .FilterDishes(restaurantId, menus, categories, name, false, isVegetarian)
            .OrderByDishSort(sort)
            .TakePage(page, pageSize)
            .ToList();

        var mapped = _mapper.Map<List<DishShortDto>>(raw);
        return Task.FromResult(new Pagination<DishShortDto>(mapped, page, pageSize, pages));
    }

    /// <summary>
    /// Get dish by id
    /// </summary>
    /// <param name="dishId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<DishFullDto> GetDish(Guid dishId) {
        var dish = await _backendDbContext.Dishes
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }

        var mapped = _mapper.Map<DishFullDto>(dish);
        return mapped;
    }

    /// <summary>
    /// Edit dish
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="dishUpdateDto"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task EditDish(Guid dishId, DishEditDto dishUpdateDto) {
        var dish = await _backendDbContext.Dishes
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }
        
        dish.Name = dishUpdateDto.Name;
        dish.Description = dishUpdateDto.Description;
        dish.Price = dishUpdateDto.Price;
        dish.IsVegetarian = dishUpdateDto.IsVegetarian;
        dish.DishCategories = dishUpdateDto.DishCategories;
        dish.UpdatedAt = DateTime.UtcNow;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Archive dish
    /// </summary>
    /// <param name="dishId"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task ArchiveDish(Guid dishId) {
        var dish = await _backendDbContext.Dishes
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }
        
        dish.IsArchived = true;
        dish.UpdatedAt = DateTime.UtcNow;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Unarchive dish
    /// </summary>
    /// <param name="dishId"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task UnarchiveDish(Guid dishId) {
        var dish = await _backendDbContext.Dishes
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }
        
        dish.IsArchived = false;
        dish.UpdatedAt = DateTime.UtcNow;
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get list of all archived dishes
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<List<DishShortDto>> GetArchivedDishes(Guid restaurantId) {
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId);

        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var dishes = await _backendDbContext.Dishes
            .Where(x => x.Menus.FirstOrDefault().RestaurantId == restaurantId
                        && x.IsArchived)
            .ToListAsync();
        
        var mapped = _mapper.Map<List<DishShortDto>>(dishes);
        return mapped;
    }

    /// <summary>
    /// Check if customer is able to set rating
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> IsAbleToSetRating(Guid dishId, Guid customerId) {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Set rating
    /// </summary>
    /// <param name="dishId"></param>
    /// <param name="customerId"></param>
    /// <param name="ratingSetDto"></param>
    /// <exception cref="NotImplementedException"></exception>
    public Task SetRating(Guid dishId, Guid customerId, RatingSetDto ratingSetDto) {
        throw new NotImplementedException();
    }
}