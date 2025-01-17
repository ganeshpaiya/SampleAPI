using Microsoft.AspNetCore.Mvc;
using SampleAPI.ViewModels;
using Service.Customers;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Order>))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await customerService.GetCustomers();

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers.Select(x => Customer.ToViewModel(x)));
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Order))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await customerService.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Customer.ToViewModel(customer));
        }

        // PUT: api/Customers/5

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var completed = await customerService.PutCustomer(id, customer.ToModel(isUpdate: true));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/Customers
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var id = await customerService.PostCustomer(customer.ToModel(isUpdate: false));

            return CreatedAtAction("PostCustomer", new { id = id }, id);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var completed = await customerService.DeleteCustomer(id);

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
