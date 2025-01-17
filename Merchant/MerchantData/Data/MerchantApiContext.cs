using MerchantData.Models;
using Microsoft.EntityFrameworkCore;

namespace MerchantData.Data
{
    public class MerchantApiContext : DbContext
    {
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        public MerchantApiContext(DbContextOptions<MerchantApiContext> options) : base(options)
        {
        }
    }
}