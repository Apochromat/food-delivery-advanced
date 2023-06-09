﻿using Delivery.BackendAPI.BL.Services;
using Delivery.BackendAPI.DAL;
using Delivery.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.BackendAPI.BL.Extensions;
/// <summary>
/// Service dependency extension
/// </summary>
public static class ServiceDependencyExtension {
    /// <summary>
    /// Add BackendAPI BL service dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddBackendBlServiceDependencies(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<BackendDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("BackendDatabasePostgres")));
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IDishService, DishService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<ITransactionValidationService, TransactionValidationService>();
        services.AddScoped<IPermissionCheckerService, PermissionCheckerService>();
        services.AddScoped<INotificationQueueService, NotificationQueueService>();
        services.AddAutoMapper(typeof(MappingProfiles.MappingProfile));
        return services;
    }
}