using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Cash
{
    public class CashService<T> : ICashService<T> where T : class
    {
        private readonly IDistributedCache distributedCache;

        public CashService(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task CashResponseAsync(string key, T respose, TimeSpan timeToLive)
        {
            if (respose == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(respose);

            await distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async  Task CashResponseAsync(string key, IEnumerable<T> respose, TimeSpan timeToLive)
        {
            if (respose == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(respose);

            await distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task<IEnumerable<T>> GetCashCollectionResponseAsync(string key)
        {
            var cashResponse = await distributedCache.GetStringAsync(key);

            if (cashResponse != null)
            {
                return !string.IsNullOrEmpty(cashResponse) ? JsonConvert.DeserializeObject<IEnumerable<T>>(cashResponse) : null;
            }
            else
            {
                return null;
            }
        }

        public async Task<T> GetCashResponseAsync(string key)
        {
            var cashResponse = await distributedCache.GetStringAsync(key);

            if (cashResponse != null)
            {
                return !string.IsNullOrEmpty(cashResponse) ? JsonConvert.DeserializeObject<T>(cashResponse) : null;
            }
            else
            {
                return null;
            }
        }

        public async Task RemoveCashResponseAsync(string key)
        {
            await distributedCache.RemoveAsync(key);
        }
    }
}
