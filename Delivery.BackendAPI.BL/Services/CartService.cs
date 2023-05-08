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
    public CartService(INotificationQueueService notificationQueueService, BackendDbContext backendDbContext, IMapper mapper) {
        _notificationQueueService = notificationQueueService;
        _backendDbContext = backendDbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Get user`s cart
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<CartDto> GetCart(Guid userId) {
        var dishes = await _backendDbContext.DishesInCart
            .Include(x=>x.Dish)
            .ThenInclude(x=>x.Menus)
            .Where(x => x.CustomerId == userId)
            .ToListAsync();
        if (dishes.Count == 0) {
            throw new NotFoundException("Cart is empty");
        }
        
        var restaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == dishes.First().Dish.Menus.First().RestaurantId);
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
    /// <exception cref="NotImplementedException"></exception>
    public async Task AddDishToCart(Guid userId, Guid dishId) {
        var dish = await _backendDbContext.Dishes
            .Include(x=>x.Menus)
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }
        
        var dishRestaurant = await _backendDbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == dish.Menus.First().RestaurantId);
        
        var cartRestaurant = await _backendDbContext.DishesInCart
            .Include(x=>x.Dish)
            .ThenInclude(x=>x.Menus)
            .FirstOrDefaultAsync(x => x.CustomerId == userId);
        
        if (cartRestaurant != null && cartRestaurant.Dish.Menus.First().RestaurantId != dishRestaurant.Id) {
            throw new BadRequestException("You can`t add dishes from different restaurants to cart");
        }
        
        var dishInCart = await _backendDbContext.DishesInCart
            .FirstOrDefaultAsync(x => x.CustomerId == userId && x.Dish.Id == dishId);
        
        if (dishInCart != null) {
            dishInCart.Amount++;
            _backendDbContext.DishesInCart.Update(dishInCart);
            await _backendDbContext.SaveChangesAsync();
            return;
        }
        
        dishInCart = new DishInCart() {
            Id = new Guid(),
            CustomerId = userId,
            Dish = dish,
            Amount = 1
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
    /// <exception cref="NotImplementedException"></exception>
    public async Task RemoveDishFromCart(Guid userId, Guid dishId, bool removeAll = false) {
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
        
        dishInCart.Amount--;
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
    /// <exception cref="NotImplementedException"></exception>
    public async Task ClearCart(Guid userId) {
        var dishesInCart = await _backendDbContext.DishesInCart
            .Where(x => x.CustomerId == userId)
            .ToListAsync();
        if (dishesInCart.Count == 0) {
            throw new NotFoundException("Cart is empty");
        }
        
        _backendDbContext.DishesInCart.RemoveRange(dishesInCart);
        await _backendDbContext.SaveChangesAsync();
    }
}