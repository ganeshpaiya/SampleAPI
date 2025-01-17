using MerchantData.Models;
using System.Threading.Tasks;

namespace MerchantService.Products
{
    public interface IProductsService
    {
        Task<Product> GetProduct(int id);

        Task<int> PostProduct(Product location);

        Task<bool> PutProduct(int id, Product location);

        Task<bool> DeleteProduct(int id);
    }
}
