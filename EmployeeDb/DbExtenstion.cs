using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MITT.EmployeeDb;

public static class DbExtenstion
{
    public static IServiceCollection AddManagementDb(this IServiceCollection services)
    {
        var employeeDbConnectionString = string.Empty;

        using (var scope = services.BuildServiceProvider()) employeeDbConnectionString = scope
                .GetService<IConfiguration>()
                .GetConnectionString(nameof(ManagementDb));

        if (string.IsNullOrEmpty(employeeDbConnectionString)) throw new Exception($"connection string for {nameof(ManagementDb)} is missing from the appsettings.json file!!");

        services.AddDbContext<ManagementDb>(opt 
            => opt.UseSqlServer(employeeDbConnectionString, x =>
                {
                    x.MigrationsHistoryTable("__MigrationsHistoryForEmployeeDbContext", "migrations");
                    x.UseHierarchyId();
                })
           .EnableDetailedErrors()
           .EnableSensitiveDataLogging());

        using (var scope = services.BuildServiceProvider())
        {
            var managementDb = scope.GetService<ManagementDb>();
            managementDb.Database?.Migrate();
        }

        return services;
    }
}