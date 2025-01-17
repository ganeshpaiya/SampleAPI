using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Customers
{
    public class CustomersService : ICustomerService
    {
        private readonly ApiContext context;

        public CustomersService(ApiContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);

            if (customer == null)
            {
                return false;
            }

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = await context.Customers.AsNoTracking().ToListAsync();
            return customers;
        }

        public async Task<int> PostCustomer(Customer customer)
        {
            context.Customers.Add(customer);
             await context.SaveChangesAsync();

            return customer.Id;
        }

        public async Task<bool> PutCustomer(int id, Customer customer)
        {
            context.Entry(customer).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        private bool CustomerExists(int id)
        {
            return context.Customers.Any(e => e.Id == id);
        }
    }
}
