using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Products
{
    public class ProductsService : IProductsService
    {
        private readonly ApiContext context;

        public ProductsService(ApiContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<int> PostProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> PutProduct(int id, Product product)
        {
            context.Entry(product).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.Id == id);
        }
    }
}
