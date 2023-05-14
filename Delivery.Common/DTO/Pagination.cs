using System.ComponentModel;

namespace Delivery.Common.DTO;

/// <summary>
/// Pagination DTO component
/// </summary>
public class Pagination<T> {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="items"></param>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="pagesAmount"></param>
    public Pagination(List<T> items, int currentPage, int pageSize, int pagesAmount) {
        Items = items;
        CurrentPage = currentPage;
        PageSize = pageSize;
        PagesAmount = pagesAmount;
    }

    /// <summary>
    /// List of items
    /// </summary>
    [DisplayName("items")]
    public List<T> Items { get; set; }

    /// <summary>
    /// Page of list (natural number)
    /// </summary>
    [DisplayName("current_page")]
    public int CurrentPage { get; set; }

    /// <summary>
    /// Count of items on page (natural number)
    /// </summary>
    [DisplayName("page_size")]
    public int PageSize { get; set; }

    /// <summary>
    /// Amount of pages
    /// </summary>
    [DisplayName("pages_amount")]
    public int PagesAmount { get; set; }
}