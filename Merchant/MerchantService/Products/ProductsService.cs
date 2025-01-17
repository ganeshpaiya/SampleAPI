using MerchantData.Data;
using MerchantData.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantService.Products
{
    public class ProductsService : IProductsService
    {
        private readonly MerchantApiContext context;

        public ProductsService(MerchantApiContext context)
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

        public async Task<int> PostProduct(Product product)
        {
            context.Products.Add(product);
            var id = await context.SaveChangesAsync();

            return id;
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
