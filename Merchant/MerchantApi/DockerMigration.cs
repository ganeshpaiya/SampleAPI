using MerchantData.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantApi
{
    public static class DockerMigration
    {
        public static void Migrate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<MerchantApiContext>());
            }
        }

        private static void SeedData(MerchantApiContext locationContext)
        {
            System.Console.WriteLine("Starting Seeding Merchant Db");
            if (locationContext.Database.GetPendingMigrations().Any())
            {
                System.Console.WriteLine("Merchant Db has migrated");
                locationContext.Database.Migrate();
            }
        }
    }
}
