using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<ProductOrder> productOrders { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) :base(options)
        {
        } 
    }
}
