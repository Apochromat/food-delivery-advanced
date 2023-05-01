using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Delivery.Common.Configurations; 

/// <summary>
/// Jwt configuration
/// </summary>
public static class JwtConfiguration {
    /// <summary>
    /// JWT lifetime in minutes
    /// </summary>
    public static int Lifetime = 5;
    /// <summary>
    /// JWT issuer
    /// </summary>
    public static string Issuer = "DefaultIssuer";
    /// <summary>
    /// JWT audience
    /// </summary>
    public static string Audience = "DefaultAudience";
    /// <summary>
    /// JWT key
    /// </summary>
    private static string _key = "An0th3rV3ryC0mpl3xS3cr3tKeyF0rJWTt0k3n";

    /// <summary>
    /// Get encoded symmetric security key
    /// </summary>
    /// <returns></returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey() {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
    }
}