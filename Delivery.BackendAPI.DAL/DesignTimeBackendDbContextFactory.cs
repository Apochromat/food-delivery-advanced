using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Delivery.BackendAPI.DAL;

/// <summary>
/// Create database context for design time
/// </summary>
public class DesignTimeBackendDbContextFactory : IDesignTimeDbContextFactory<BackendDbContext> {
    /// <summary>
    /// Create database context
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public BackendDbContext CreateDbContext(string[] args) {
        var builder = new DbContextOptionsBuilder<BackendDbContext>();
        var connectionString = "Host=localhost;Database=delivery-backend-db;Username=postgres;Password=postgres";
        return new BackendDbContext(builder.Options, connectionString);
    }
}