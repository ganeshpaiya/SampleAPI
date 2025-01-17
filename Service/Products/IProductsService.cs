using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;

namespace Service.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int id);

        Task<bool> PutProduct(int id, Product product);

        Task<int> PostProduct(Product product);

        Task<bool> DeleteProduct(int id);
    }
}
