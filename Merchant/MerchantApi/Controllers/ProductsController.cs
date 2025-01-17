using AutoMapper;
using MerchantAbstraction.ViewModels.Products;
using MerchantData.Models;
using MerchantService.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace MerchantApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly IMapper mapper;

        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            this.productsService = productsService;
            this.mapper = mapper;
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ProductDTO))]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {

            var product = await productsService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<ProductDTO>(product));
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var completed = await productsService.PutProduct(id, mapper.Map<Product>(product));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/products
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult> PostMerchant(CreateProductDTO product)
        {
            var id = await productsService.PostProduct(mapper.Map<Product>(product));


            return CreatedAtAction("GetProduct", new { id = id }, id);
        }

        // DELETE: api/products/5
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
