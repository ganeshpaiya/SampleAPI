using Data.Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Orders
{
    public class OrdersService : IOrdersService
    {

        private readonly ApiContext context;
        public OrdersService(ApiContext context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            context.Orders.Remove(order);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Order> GetOrder(int id)
        {
            var order = await context.Orders
                .AsNoTracking()
                .Include(a => a.ProductOrders)
                .ThenInclude(b=>b.Product)
                .Where(q => q.Id == id)
                .FirstOrDefaultAsync();
                
            return order;
        }

        public async Task<Order> GetOrdersByCustomer(int customerId)
        {
            var order = await context.Orders
                .AsNoTracking()
                .Include(a => a.ProductOrders)
                .ThenInclude(b => b.Product)
                .Where(q => q.CustomerId == customerId)
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<int> PostOrder(Order order)
        {
            context.Orders.Add(order);

            context.productOrders
                .AddRange(order.ProductOrders.Select(c => { c.OrderId = order.Id; return c; }).ToList());

            await context.SaveChangesAsync();

            return order.Id;
        }

        public async Task<bool> PutOrder(int id, Order order)
        {
            context.Entry(order).State = EntityState.Modified;
            order.ProductOrders.ToList().ForEach(l => context.Entry(l).State = EntityState.Modified);


            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageOrder(id))
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

        private bool ImageOrder(int id)
        {
            return context.Orders.Any(e => e.Id == id);
        }
    }
}
