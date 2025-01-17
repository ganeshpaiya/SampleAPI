using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleAPI.Cash;
using SampleAPI.ViewModels;
using Service.Cash;
using Service.Images;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService imagesService;
        private readonly ICashService<Image> cashService;
        private readonly IConfiguration Configuration;
        private readonly TimeSpan timeTolive;

        public ImagesController(IImagesService imagesService,
            ICashService<Image> cashService,
            IConfiguration Configuration)
        {
            this.imagesService = imagesService;
            this.cashService = cashService;
            this.Configuration = Configuration;
            TimeSpan.TryParse(Configuration["TimeTolive"].Trim(), out timeTolive);
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Image))]
        public async Task<ActionResult<Image>> GetImage(int id)
        {

            var cashedImage = await cashService.GetCashResponseAsync(CashUtilities.GenerateKeyCashKeyFromRequest(this.Request));

            if (cashedImage != null)
            {
                return Ok(cashedImage);
            }

            var image = await imagesService.GetImage(id);

            if (image == null)
            {
                return NotFound();
            }

            await cashService.CashResponseAsync(
               CashUtilities.GenerateKeyCashKeyFromRequest(this.Request),
                Image.ToViewModel(image),
                timeTolive);

            return Ok(Image.ToViewModel(image));
        }

        // GET: api/product/5
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Image>))]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<Image>> GetproductImages(int productId)
        {
            var cashedImages = await cashService.GetCashCollectionResponseAsync(CashUtilities.GenerateKeyCashKeyFromRequest(this.Request));

            if (cashedImages != null)
            {
                return Ok(cashedImages);
            }

            var images = await imagesService.GetproductImages(productId);

            if (images == null)
            {
                return NotFound();
            }

            await cashService.CashResponseAsync(
             CashUtilities.GenerateKeyCashKeyFromRequest(this.Request),
              images.Select(x => Image.ToViewModel(x)),
              timeTolive);

            return Ok(images.Select(x => Image.ToViewModel(x)));
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutImage(int id, Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            var completed = await imagesService.PutImage(id, image.ToModel(isUpdate: true));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                await cashService.RemoveCashResponseAsync(CashUtilities.GenerateKeyCashKeyFromRequest(this.Request));

                return Ok();
            }
        }

        // POST: api/Images
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            var id = await imagesService.PostImage(image.ToModel(isUpdate: false));


            return CreatedAtAction("GetImage", new { id = image.Id }, id);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult<Image>> DeleteImage(int id)
        {
            var completed = await imagesService.DeleteImage(id);

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                await cashService.RemoveCashResponseAsync(CashUtilities.GenerateKeyCashKeyFromRequest(this.Request));

                return Ok();
            }
        }
    }
}
