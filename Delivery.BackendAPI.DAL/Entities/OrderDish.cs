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
    public required Dish Dish { get; set; }

    /// <summary>
    /// Order of the dish
    /// </summary>
    public Order Order { get; set; } = null!;   // Late init

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
    public string? ArchivedDishName { get; set; }

    /// <summary>
    /// Archived dish image url
    /// </summary>
    public string? ArchivedDishImageUrl { get; set; }

    /// <summary>
    /// Archived dish description
    /// </summary>
    public string? ArchivedDishDescription { get; set; }
}