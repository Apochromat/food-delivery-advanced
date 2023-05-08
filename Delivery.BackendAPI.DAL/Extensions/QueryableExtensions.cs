using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.Enums;

namespace Delivery.BackendAPI.DAL.Extensions; 

/// <summary>
/// Queryable extensions for LINQ
/// </summary>
public static class QueryableExtensions {
    
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
    
    /// <summary>
    /// Filter dishes
    /// </summary>
    public static IQueryable<Dish> FilterDishes(this IQueryable<Dish> source, Guid restaurantId, List<Guid>? menus, List<DishCategory>? categories, string? name, bool isArchived = false, bool isVegetarian = false) {
        return source.Where(x => 
            x.Menus.FirstOrDefault().RestaurantId == restaurantId
            && x.Menus.Any(y => menus == null|| menus.Count == 0 || menus.Contains(y.Id))
            && x.IsArchived == isArchived
            && x.IsVegetarian == isVegetarian
            && (categories == null || categories.Count == 0 || categories.All(y => x.DishCategories.Contains(y)))
            && (name == null || x.Name.Contains(name)));
    }
    
    
}