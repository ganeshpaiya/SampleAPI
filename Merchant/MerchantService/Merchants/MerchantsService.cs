using MerchantData.Data;
using MerchantData.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantService.Merchants
{
    public class MerchantsService : IMerchantsService
    {
        private readonly MerchantApiContext context;

        public MerchantsService(MerchantApiContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteMerchant(int id)
        {
            var merchant = await context.Merchants.FindAsync(id);

            if (merchant == null)
            {
                return false;
            }

            context.Merchants.Remove(merchant);
            await context.SaveChangesAsync();


            return true;
        }

        public async Task<Merchant> GetMerchant(int id)
        {
            var merchant = await context.Merchants.FindAsync(id);
            return merchant;
        }

        public async Task<int> PostMerchant(Merchant merchant)
        {
            context.Merchants.Add(merchant);
            var id = await context.SaveChangesAsync();

            return id;
        }

        public async Task<bool> PutMerchant(int id, Merchant merchant)
        {
            context.Entry(merchant).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantExists(id))
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

        private bool MerchantExists(int id)
        {
            return context.Merchants.Any(e => e.Id == id);
        }
    }
}
