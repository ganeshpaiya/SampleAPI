using LookUpData.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookUpApi
{
    public static class DockerMigration
    {
        public static void Migrate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<LookUpDbContext>());
            }
        }

        private static void SeedData(LookUpDbContext locationContext)
        {
            System.Console.WriteLine("Starting Seeding LookUp Db");
            if (locationContext.Database.GetPendingMigrations().Any())
            {
                System.Console.WriteLine("LookUp Db has migrated");
                locationContext.Database.Migrate();
            }
        }
    }
}
