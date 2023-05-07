using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.Enums;

namespace Delivery.BackendAPI.DAL.Extensions; 

/// <summary>
/// Enumerable extensions for LINQ
/// </summary>
public static class EnumerableExtensions {
    
    /// <summary>
    /// Order smth by OrderSort enum
    /// </summary>
    public static IQueryable<Order> OrderByOrderSort(this IQueryable<Order> source, OrderSort? sort) {
        return sort switch {
            OrderSort.CreationAsc => source.OrderBy(x => x.OrderTime),
            OrderSort.CreationDesc => source.OrderByDescending(x => x.OrderTime),
            OrderSort.DeliveryAsc => source.OrderBy(x => x.DeliveryTime),
            OrderSort.DeliveryDesc => source.OrderByDescending(x => x.DeliveryTime),
            _ => source
        };
    }
    
    /// <summary>
    /// Order smth by DishSort enum
    /// </summary>
    public static IQueryable<Dish> OrderByDishSort(this IQueryable<Dish> source, DishSort? sort) {
        return sort switch {
            DishSort.NameAsc => source.OrderBy(x => x.Name),
            DishSort.NameDesc => source.OrderByDescending(x => x.Name),
            DishSort.PriceAsc => source.OrderBy(x => x.Price),
            DishSort.PriceDesc => source.OrderByDescending(x => x.Price),
            DishSort.RatingAsc => source.OrderBy(x => x.CalculatedRating),
            DishSort.RatingDesc => source.OrderByDescending(x => x.CalculatedRating),
            _ => source
        };
    }
    
    /// <summary>
    /// Order smth by RestaurantSort enum
    /// </summary>
    public static IQueryable<Restaurant> OrderByRestaurantSort(this IQueryable<Restaurant> source, RestaurantSort? sort) {
        return sort switch {
            RestaurantSort.NameAsc => source.OrderBy(x => x.Name),
            RestaurantSort.NameDesc => source.OrderByDescending(x => x.Name),
            _ => source
        };
    }
}