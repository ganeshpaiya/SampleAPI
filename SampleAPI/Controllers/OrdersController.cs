using Microsoft.AspNetCore.Mvc;
using SampleAPI.ViewModels;
using Service.Orders;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Order))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await ordersService.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(Order.ToViewModel(order));
        }

        // GET: api/product/customer/5
        [HttpGet("customer/{customerId}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Order))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<Order>> GetCustomerOrders(int customerId)
        {
            var order = await ordersService.GetOrdersByCustomer(customerId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(Order.ToViewModel(order));
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var completed = await ordersService.PutOrder(id, order.ToModel(isUpdate: true));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/Orders
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            var id = await ordersService.PostOrder(order.ToModel(isUpdate: false));

            return CreatedAtAction("PostOrder", new { id = id }, id);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var completed = await ordersService.DeleteOrder(id);

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
