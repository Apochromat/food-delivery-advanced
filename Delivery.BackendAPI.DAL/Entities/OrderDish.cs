namespace Delivery.BackendAPI.DAL.Entities; 

/// <summary>
/// Entity for dishes in order
/// </summary>
public class OrderDish {
    /// <summary>
    /// OrderDish identifier
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Dish in order identifier
    /// </summary>
    public Dish? Dish { get; set; }
    /// <summary>
    /// Order of the dish
    /// </summary>
    public Order? Order { get; set; }
    
    /// <summary>
    /// Amount of dishes in order
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Archived dish price
    /// </summary>
    public decimal ArchivedDishPrice { get; set; }
    /// <summary>
    /// Archived dish name
    /// </summary>
    public String? ArchivedDishName { get; set; }
}