using AutoMapper;
using MerchantAbstraction.ViewModels.Merchants;
using MerchantData.Models;
using MerchantService.Merchants;
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
    public class MerchantsController : ControllerBase
    {

        private readonly IMerchantsService merchantsService;
        private readonly IMapper mapper;

        public MerchantsController(IMerchantsService merchantsService, IMapper mapper)
        {
            this.merchantsService = merchantsService;
            this.mapper = mapper;
        }

        // GET: api/merchants/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(MerchantDTO))]
        public async Task<ActionResult<MerchantDTO>> GetMerchant(int id)
        {

            var merchant = await merchantsService.GetMerchant(id);

            if (merchant == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<MerchantDTO>(merchant));
        }

        // PUT: api/merchants/5
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutMerchant(int id, UpdateMerchantDTO merchant)
        {
            if (id != merchant.Id)
            {
                return BadRequest();
            }

            var completed = await merchantsService.PutMerchant(id, mapper.Map<Merchant>(merchant));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/merchants
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult> PostMerchant(CreateMerchantDTO merchant)
        {
            var id = await merchantsService.PostMerchant(mapper.Map<Merchant>(merchant));


            return CreatedAtAction("GetMerchant", new { id = id }, id);
        }

        // DELETE: api/merchants/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteMerchant(int id)
        {
            var completed = await merchantsService.DeleteMerchant(id);

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
