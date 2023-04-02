using System.ComponentModel;

namespace Delivery.Common.DTO; 

/// <summary>
/// Pagination DTO component
/// </summary>
public class Pagination<T> {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="content"></param>
    /// <param name="current"></param>
    /// <param name="items"></param>
    /// <param name="pages"></param>
    public Pagination(List<T> content, int current, int items, int pages) {
        Content = content;
        Current = current;
        Items = items;
        Pages = pages;
    }
    
    /// <summary>
    /// Constructor
    /// </summary>
    public Pagination() {
    }

    /// <summary>
    /// List of items
    /// </summary>
    [DisplayName("content")]
    public List<T> Content { get; set; } = new List<T>();

    /// <summary>
    /// Page of list (natural number)
    /// </summary>
    [DisplayName("current")]
    public int Current { get; set; }
    
    /// <summary>
    /// Count of items on page (natural number)
    /// </summary>
    [DisplayName("items")]
    public int Items { get; set; }
    
    /// <summary>
    /// Amount of pages
    /// </summary>
    [DisplayName("pages")]
    public int Pages { get; set; }
}