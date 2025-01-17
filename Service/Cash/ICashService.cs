using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Cash
{
    public interface ICashService<T> where T : class
    {
        Task CashResponseAsync(string key, T respose, TimeSpan timeToLive);

        Task CashResponseAsync(string key, IEnumerable<T> respose, TimeSpan timeToLive);

        Task<T> GetCashResponseAsync(string key);

        Task<IEnumerable<T>> GetCashCollectionResponseAsync(string key);

        Task RemoveCashResponseAsync(string key);
    }
}
