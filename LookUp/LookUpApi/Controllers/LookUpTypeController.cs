using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LookUpAbstraction.DTO.LookUpType.Request;
using LookUpAbstraction.DTO.LookUpType.Response;
using LookUpData.Models;
using LookUpService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LookUpApi.Controllers
{
    [Route("api/[controller]")]
    public class LookUpTypeController : Controller
    {
        private readonly ILookUpTypesService lookUpTypesService;
        private readonly IMapper mapper;

        public LookUpTypeController(ILookUpTypesService lookUpTypesService, IMapper mapper)
        {
            this.lookUpTypesService = lookUpTypesService;
            this.mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<LookUpTypeDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetLookUpTypes()
        {
            var lookUpTypes = await lookUpTypesService.GetLookUpTypes();

            if (lookUpTypes != null)
                return Ok(mapper.Map<List<LookUpTypeDTO>>(lookUpTypes));

            return NotFound();

        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(LookUpTypeDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetLookUpType(int id)
        {
            var lookUpType = await lookUpTypesService.GetLookUpType(id);

            if (lookUpType != null)
                return Ok(mapper.Map<LookUpTypeDTO>(lookUpType));

            return NotFound();

        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<ActionResult> PostLookUpType([FromBody]CreateLookUpTypeDTO createLookUpTypeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lookUpTypeId = await lookUpTypesService.PostLookUpType(mapper.Map<LookUpType>(createLookUpTypeDTO));

            if (lookUpTypeId != default)
                return Created("LookUpType Id :", lookUpTypeId);
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> PutLookUpType(int id, [FromBody] UpdateLookUpTypeDTO updateLookUpTypeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var success = await lookUpTypesService.PutLookUpType(id, mapper.Map<LookUpType>(updateLookUpTypeDTO));

                if (success)
                    return Ok(id);
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteLookUpType(int id)
        {
            var success = await lookUpTypesService.DeleteLookUpType(id);

            if (success)
                return Ok(id);
            else
                return NotFound();
        }
    }
}
