namespace Delivery.Common.Extensions;

/// <summary>
/// Queryable extensions for LINQ
/// </summary>
public static class QueryableExtensions {
    /// <summary>
    /// Order smth by OrderSort enum
    /// </summary>
    public static IQueryable<T> TakePage<T>(this IQueryable<T> source, int page, int pageSize) {
        return source
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }
}