using Delivery.Common.DTO;

namespace Delivery.Common.Interfaces;

/// <summary>
/// Rating service interface
/// </summary>
public interface IRatingService {
    /// <summary>
    /// Check if user can rate dish
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <returns></returns>
    Task<RatingCheckDto> IsUserCanRate(Guid userId, Guid dishId);

    /// <summary>
    /// Rate dish
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="dishId"></param>
    /// <param name="rating"></param>
    /// <returns></returns>
    Task RateDish(Guid userId, Guid dishId, RatingSetDto rating);
}