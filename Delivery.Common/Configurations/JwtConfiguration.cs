using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Delivery.Common.Configurations; 

/// <summary>
/// Jwt configuration
/// </summary>
public static class JwtConfiguration {
    /// <summary>
    /// JWT lifetime in minutes
    /// </summary>
    public static int Lifetime = 10;
    /// <summary>
    /// JWT issuer
    /// </summary>
    public static string Issuer = "http://localhost:5000";
    /// <summary>
    /// JWT audience
    /// </summary>
    public static string Audience = "http://localhost:5000";
    /// <summary>
    /// JWT key
    /// </summary>
    private static string _key = "An0th3rV3ryC0mpl3xS3cr3tKeyF0rJWTt0k3n";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
    }
}