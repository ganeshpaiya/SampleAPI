using LocationData.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace LocationAPI
{
    public static class DockerMigration
    {
        public static void Migrate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<LocationContext>());
            }
        }

        private static void SeedData(LocationContext locationContext)
        {
            System.Console.WriteLine("Starting Seeding Location db");
            if (locationContext.Database.GetPendingMigrations().Any())
            {
                System.Console.WriteLine("Location db has migrated");
                locationContext.Database.Migrate();
            }
        }
    }
}
