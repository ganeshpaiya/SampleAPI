using AutoMapper;
using MerchantAbstraction.ViewModels.Images;
using MerchantData.Models;
using MerchantService.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MerchantApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService imagesService;
        private readonly IMapper mapper;

        public ImagesController(IImagesService imagesService, IMapper mapper)
        {
            this.imagesService = imagesService;
            this.mapper = mapper;
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ImageDTO))]
        public async Task<ActionResult<ImageDTO>> GetImage(int id)
        {

            var image = await imagesService.GetImage(id);

            if (image == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<ImageDTO>(image));
        }

        // GET: api/product/5
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<ImageDTO>))]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<ImageDTO>> GetproductImages(int productId)
        {          
            var images = await imagesService.GetproductImages(productId);

            if (images == null)
            {
                return NotFound();
            }


            return Ok(images.Select(x => mapper.Map<ImageDTO>(x)));
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutImage(int id, UpdateImageDTO image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            var completed = await imagesService.PutImage(id, mapper.Map<Image>(image));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/Images
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult> PostImage(CreateImageDTO image)
        {
            var id = await imagesService.PostImage(mapper.Map<Image>(image));


            return CreatedAtAction("GetImage", new { id = id }, id);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteImage(int id)
        {
            var completed = await imagesService.DeleteImage(id);

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