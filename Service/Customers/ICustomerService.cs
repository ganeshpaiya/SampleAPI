using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Customers
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomer(int id);

        Task<IEnumerable<Customer>> GetCustomers();

        Task<bool> PutCustomer(int id, Customer customer);

        Task<int> PostCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);
    }
}
