using AutoMapper;
using Delivery.BackendAPI.DAL;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BackendAPI.BL.Services; 

/// <summary>
/// Cart service
/// </summary>
public class CartService : ICartService {
    private readonly INotificationQueueService _notificationQueueService;
    private readonly BackendDbContext _backendDbContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="notificationQueueService"></param>
    /// <param name="backendDbContext"></param>
    /// <param name="mapper"></param>
    public CartService(INotificationQueueService notificationQueueService, BackendDbContext backendDbContext, IMapper mapper) {
        _notificationQueueService = notificationQueueService;
        _backendDbContext = backendDbContext;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task ClearCartFromArchivedDishes(Guid userId) {
        var dishes = await _backendDbContext.DishesInCart
            .Include(x=>x.Dish)
            .Include(x=>x.Restaurant)
            .Where(x => x.CustomerId == userId)
            .ToListAsync();
        if (dishes.Count == 0) {
            return;
        }
        
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == dishes.First().Restaurant.Id);
        if (restaurant == null || restaurant.IsArchived) {
            await ClearCart(userId);
        }

        foreach (var dish in dishes) {
            if (dish.Dish.IsArchived) {
                await RemoveDishFromCart(userId, dish.Dish.Id, true);
            }
        }
    }

    /// <summary>
    /// Get user`s cart
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<CartDto> GetCart(Guid userId) {
        await ClearCartFromArchivedDishes(userId);
        var dishes = await _backendDbContext.DishesInCart
            .Include(x=>x.Dish)
            .Include(x=>x.Restaurant)
            .Where(x => x.CustomerId == userId)
            .ToListAsync();
        if (dishes.Count == 0) {
            throw new NotFoundException("Cart is empty");
        }
        
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == dishes.First().Restaurant.Id);
        if (restaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var cart = new CartDto() { 
            Dishes = _mapper.Map<List<CartDishDto>>(dishes),
            Restaurant = _mapper.Map<RestaurantShortDto>(restaurant),
            TotalPrice = dishes.Sum(x=>x.Dish.Price*x.Amount)
        };
        return cart;
    }

    /// <summary>
    /// Add dish to cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <param name="amount"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task AddDishToCart(Guid userId, Guid dishId, int amount = 1) {
        var dish = await _backendDbContext.Dishes
            .Include(x=>x.Menus)
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }
        
        var dishRestaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == dish.Menus.First().RestaurantId);
        
        if (dishRestaurant == null) {
            throw new NotFoundException("Restaurant not found");
        }
        
        var cartRestaurant = await _backendDbContext.DishesInCart
            .Include(x=>x.Restaurant)
            .FirstOrDefaultAsync(x => x.CustomerId == userId);
        
        if (cartRestaurant != null && cartRestaurant.Restaurant.Id != dishRestaurant.Id) {
            throw new BadRequestException("You can`t add dishes from different restaurants to cart");
        }
        
        var dishInCart = await _backendDbContext.DishesInCart
            .FirstOrDefaultAsync(x => x.CustomerId == userId && x.Dish.Id == dishId);
        
        if (dishInCart != null) {
            dishInCart.Amount += amount;
            _backendDbContext.DishesInCart.Update(dishInCart);
            await _backendDbContext.SaveChangesAsync();
            return;
        }
        
        dishInCart = new DishInCart() {
            Id = new Guid(),
            CustomerId = userId,
            Dish = dish,
            Amount = amount,
            Restaurant = dishRestaurant
        };
        
        await _backendDbContext.DishesInCart.AddAsync(dishInCart);
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Remove dish from cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <param name="removeAll"></param>
    /// <param name="amount"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RemoveDishFromCart(Guid userId, Guid dishId, bool removeAll = false, int amount = 1) {
        var dishInCart = await _backendDbContext.DishesInCart
            .FirstOrDefaultAsync(x => x.CustomerId == userId && x.Dish.Id == dishId);
        if (dishInCart == null) {
            throw new NotFoundException("Dish not found in cart");
        }
        
        if (removeAll) {
            _backendDbContext.DishesInCart.Remove(dishInCart);
            await _backendDbContext.SaveChangesAsync();
            return;
        }
        
        if (dishInCart.Amount < amount) {
            throw new BadRequestException("You can`t remove more dishes than you have");
        }
        dishInCart.Amount -= amount;
        if (dishInCart.Amount == 0) {
            _backendDbContext.DishesInCart.Remove(dishInCart);
        }
        else {
            _backendDbContext.DishesInCart.Update(dishInCart);
        }
        await _backendDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Clear cart
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="force"></param>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ClearCart(Guid userId, bool force = true) {
        var dishesInCart = await _backendDbContext.DishesInCart
            .Where(x => x.CustomerId == userId)
            .ToListAsync();
        if (dishesInCart.Count == 0 && force) {
            throw new NotFoundException("Cart is empty");
        }
        if (!force) {
            return;
        }
        
        _backendDbContext.DishesInCart.RemoveRange(dishesInCart);
        await _backendDbContext.SaveChangesAsync();
    }
}