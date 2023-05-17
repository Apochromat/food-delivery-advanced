using Delivery.BackendAPI.DAL;
using Delivery.BackendAPI.DAL.Entities;
using Delivery.Common.DTO;
using Delivery.Common.Enums;
using Delivery.Common.Exceptions;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BackendAPI.BL.Services;

/// <summary>
/// Rating service
/// </summary>
public class RatingService : IRatingService {
    private readonly BackendDbContext _backendDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backendDbContext"></param>
    public RatingService(BackendDbContext backendDbContext) {
        _backendDbContext = backendDbContext;
    }

    ///<inheritdoc/>
    public async Task<RatingCheckDto> IsUserCanRate(Guid userId, Guid dishId) {
        var isOrdered = await _backendDbContext.Orders
            .Where(x =>
                x.CustomerId == userId
                && x.Status == OrderStatus.Delivered)
            .SelectMany(x => x.Dishes)
            .Select(x => x.Dish.Id)
            .Where(d => d == dishId)
            .CountAsync();

        return new RatingCheckDto() { IsAbleToRate = isOrdered > 0 };
    }

    ///<inheritdoc/>
    public async Task RateDish(Guid userId, Guid dishId, RatingSetDto rating) {
        var isOrdered = await _backendDbContext.Orders
            .Where(x =>
                x.CustomerId == userId
                && x.Status == OrderStatus.Delivered)
            .SelectMany(x => x.Dishes)
            .Select(x => x.Dish.Id)
            .Where(d => d == dishId)
            .CountAsync();

        if (isOrdered == 0) {
            throw new ForbiddenException("You can rate only ordered dishes");
        }

        if (rating.Rating < 1 || rating.Rating > 5) {
            throw new BadRequestException("Rating must be between 1 and 5");
        }

        var dish = await _backendDbContext.Dishes
            .FirstOrDefaultAsync(x => x.Id == dishId);
        if (dish == null) {
            throw new NotFoundException("Dish not found");
        }

        var userRating = await _backendDbContext.Ratings
            .FirstOrDefaultAsync(x => x.CustomerId == userId && x.Dish.Id == dishId);

        if (userRating == null) {
            userRating = new Rating {
                Id = new Guid(),
                CustomerId = userId,
                Dish = dish,
                Value = rating.Rating
            };
            await _backendDbContext.Ratings.AddAsync(userRating);
        }
        else {
            userRating.Value = rating.Rating;
        }

        if (await _backendDbContext.Ratings
                .Where(x => x.Dish.Id == dishId)
                .CountAsync() > 0) {
            var dishCalculatedRating = await _backendDbContext.Ratings
                .Where(x => x.Dish.Id == dishId)
                .AverageAsync(x => x.Value);

            dish.CalculatedRating = (decimal)dishCalculatedRating;
        }
        else {
            dish.CalculatedRating = rating.Rating;
        }

        await _backendDbContext.SaveChangesAsync();
    }
}