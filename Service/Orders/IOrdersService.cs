using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Orders
{
    public interface IOrdersService
    {
        Task<Order> GetOrdersByCustomer( int customerId);

        Task<Order> GetOrder(int id);

        Task<bool> PutOrder(int id, Order order);

        Task<int> PostOrder(Order order);

        Task<bool> DeleteOrder(int id);
    }
}
