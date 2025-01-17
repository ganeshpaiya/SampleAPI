using MerchantData.Models;
using System.Threading.Tasks;

namespace MerchantService.Merchants
{
    public interface IMerchantsService
    {
        Task<Merchant> GetMerchant(int id);

        Task<int> PostMerchant(Merchant location);

        Task<bool> PutMerchant(int id, Merchant location);

        Task<bool> DeleteMerchant(int id);
    }
}
