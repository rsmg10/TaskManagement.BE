using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MITT.EmployeeDb;
using MITT.Services.Abstracts;
using MITT.Services.Helpers.JwtHelper;
using MITT.Services.TaskServices;

namespace MITT.Services;

public static class Extenstion
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddManagementDb();
        services.AddJwt();
        services.AddCorsPolicy();

        return services.AddTransient<IDeveloperService, DeveloperService>()
            .AddTransient<ITaskService, TaskService>()
            .AddTransient<AssignmentService>()
            .AddTransient<ReviewService>()
            .AddTransient<IProjectsService, ProjectsService>()
            .AddTransient<IManagerService, ManagerService>()
            .AddTransient<IAuthService, AuthService>();
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        var origins = services.ConfigureCors();
        return services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
            .WithOrigins(origins)
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader()));
    }

    private static string[] ConfigureCors(this IServiceCollection services)
    {
        List<string> options;
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var configuration = serviceProvider.GetService<IConfiguration>();

            var config = configuration?.GetSection("SpecificOrigins");

            services.Configure<List<string>>(config);
            options = configuration.GetOptions<List<string>>("SpecificOrigins");
        }

        return options.ToArray();
    }

    public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
    {
        var model = new TModel();
        configuration.GetSection(section).Bind(model);
        return model;
    }
}