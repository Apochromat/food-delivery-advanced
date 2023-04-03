using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Delivery.AuthAPI.DAL;

/// <summary>
/// Create database context for design time
/// </summary>
public class DesignTimeAuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext> {
    /// <summary>
    /// Create database context
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public AuthDbContext CreateDbContext(string[] args) {
        var builder = new DbContextOptionsBuilder<AuthDbContext>();
        return new AuthDbContext(builder.Options);
    }
}