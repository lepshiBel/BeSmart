using BeSmart.Domain.Interfaces;
using BeSmart.Persistence.Data;
using BeSmart.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.WebApi.Extensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        services.AddDbContext<BeSmartDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString, opt =>
            {
                opt.MigrationsAssembly("BeSmart.WebApi");
            });
        });

        return services;
    }
}