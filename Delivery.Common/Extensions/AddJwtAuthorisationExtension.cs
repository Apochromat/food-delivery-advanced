﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Delivery.Common.Extensions;

/// <summary>
/// 
/// </summary>
public static class AddJwtAuthorisationExtension {
    /// <summary>
    /// Add BackendAPI BL service dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtAuthorisation(this IServiceCollection services, IConfiguration configuration) {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("Jwt")["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("Jwt")["Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration.GetSection("Jwt")["Secret"] ?? string.Empty)),
                    ValidateIssuerSigningKey = true,
                };
            });
        return services;
    }
}