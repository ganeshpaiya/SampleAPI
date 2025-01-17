using AutoMapper;
using LookUpAbstraction.DTO.CascadingLookUp.Request;
using LookUpAbstraction.DTO.CascadingLookUp.Response;
using LookUpData.Models;
using LookUpService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LookUpApi.Controllers
{
    [Route("api/[controller]")]
    public class CascadingLookUpController : Controller
    {
        private readonly ICascadingLookUpsService cascadingLookUpService;
        private readonly IMapper mapper;

        public CascadingLookUpController(ICascadingLookUpsService cascadingLookUpService, IMapper mapper)
        {
            this.cascadingLookUpService = cascadingLookUpService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<CascadingLookUpDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCascadingLookUp(int id)
        {
            var cascadingLookUp = await cascadingLookUpService.GetCascadingLookUp(id);

            if (cascadingLookUp != null)
                return Ok(mapper.Map<CascadingLookUpDTO>(cascadingLookUp));

            return NotFound();

        }

        [HttpGet("parent/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<CascadingLookUpDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetCascadingLookUpForParentId(int id)
        {
            var cascadingLookUp = await cascadingLookUpService.GetCascadingLookUpsForParent(id);

            if (cascadingLookUp != null)
                return Ok(mapper.Map<List<CascadingLookUpDTO>>(cascadingLookUp));

            return NotFound();
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<ActionResult> PostCascadingLookUp([FromBody]CreateCascadingLookUpDTO createCascadingLookUpDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cascadingLookUpId = await cascadingLookUpService.
                PostCascadingLookUp(mapper.Map<CascadingLookUp>(createCascadingLookUpDTO));

            if (cascadingLookUpId != default)
                return Created("CascadingLookUp Id :", cascadingLookUpId);
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> PutCascadingLookUp(int id, [FromBody]UpdateCascadingLookUpDTO UpdateCascadingLookUpDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var success = await cascadingLookUpService.PutCascadingLookUp(id, mapper.Map<CascadingLookUp>(UpdateCascadingLookUpDTO));

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
        public async Task<ActionResult> DeleteCascadingLookUp(int id)
        {
            var success = await cascadingLookUpService.DeleteCascadingLookUp(id);

            if (success)
                return Ok(id);
            else
                return NotFound();
        }
    }
}
