using Microsoft.AspNetCore.Mvc;
using SampleAPI.ViewModels;
using Service.Products;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        // GET: api/Products
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await productsService.GetProducts();
            return Ok(products.Select(x => Product.ToViewModel(x)));
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Product))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productsService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(Product.ToViewModel(product));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var completed = await productsService.PutProduct(id, product.ToModel(isUpdate: true));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/Products
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult<int>> PostProduct(Product product)
        {
            var id = await productsService.PostProduct(product.ToModel(isUpdate: false));

            return CreatedAtAction("GetProduct", new { id = id }, id);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var completed = await productsService.DeleteProduct(id);
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
