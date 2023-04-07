using Delivery.Common.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    /// <returns></returns>
    public static IServiceCollection AddJwtAuthorisation(this IServiceCollection services) {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidIssuer = JwtConfiguration.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtConfiguration.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = JwtConfiguration.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
        return services;
    }
}