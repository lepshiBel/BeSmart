using BeSmart.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.WebApi.Extensions
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BeSmartDbContext>();
            context.Database.Migrate();
        }
    }
}

//serviceScope.ServiceProvider.GetService<BeSmartDbContext>().Database.Migrate();
//var service = serviceScope.ServiceProvider;

//var serviceProvider = services.BuildServiceProvider();
//using var serviceScope = serviceProvider.CreateScope();
//var context = serviceScope.ServiceProvider.GetService<BeSmartDbContext>();
//context.Database.Migrate();