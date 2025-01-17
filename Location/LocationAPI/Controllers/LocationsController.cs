using AutoMapper;
using LocationAbstraction.ViewModels.Locations;
using LocationData.Models;
using LocationService.Locations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace LocationAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService locationsService;
        private readonly IMapper mapper;

        public LocationsController(ILocationsService locationsService, IMapper mapper)
        {
            this.locationsService = locationsService;
            this.mapper = mapper;
        }

        // GET: api/location/5
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(LocationDTO))]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var location = await locationsService.GetLocation(id);

            if (location == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<LocationDTO>(location));
        }

        // PUT: api/location/5
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutLocation(int id, UpdateLocationDTO location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            var completed = await locationsService.PutLocation(id, mapper.Map<Location>(location));

            if (!completed)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // POST: api/location
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(int))]
        public async Task<ActionResult> PostLocation(CreateLocationDTO image)
        {
            var id = await locationsService.PostLocation(mapper.Map<Location>(image));


            return CreatedAtAction("PostLocation", new { id = id }, id);
        }

        // DELETE: api/location/5
        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteLocation(int id)
        {
            var completed = await locationsService.DeleteLocation(id);

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