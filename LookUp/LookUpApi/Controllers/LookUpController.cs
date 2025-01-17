using AutoMapper;
using LookUpAbstraction.DTO.LookUp.Request;
using LookUpAbstraction.DTO.LookUp.Response;
using LookUpData.Models;
using LookUpService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LookUpApi.Controllers
{
    [Route("api/[controller]")]
    public class LookUpController : Controller
    {
        private readonly ILookUpsService lookUpService;
        private readonly IMapper mapper;

        public LookUpController(ILookUpsService lookUpService, IMapper mapper)
        {
            this.lookUpService = lookUpService;
            this.mapper = mapper;
        }

        [HttpGet("type/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(List<LookUpDTO>))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetLookUpOfType(int id)
        {
            var lookUps = await lookUpService.GetLookUps(id);

            if (lookUps != null)
                return Ok(mapper.Map<List<LookUpDTO>>(lookUps));

            return NotFound();

        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(LookUpDTO))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetLookUp(int id)
        {
            var lookup = await lookUpService.GetLookUp(id);

            if (lookup != null)
                return Ok(mapper.Map<LookUpDTO>(lookup));

            return NotFound();
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public async Task<ActionResult> PostLookUp([FromBody]CreateLookUpDTO createLookUpDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lookUpId = await lookUpService.PostLookUp(mapper.Map<LookUp>(createLookUpDTO));

            if (lookUpId != default)
                return Created("LookUp Id :", lookUpId);
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(int))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(string))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<ActionResult> PutLookUp(int id, [FromBody]UpdateLookUpDTO updateLookUpDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var success = await lookUpService.PutLookUp(id, mapper.Map<LookUp>(updateLookUpDTO));

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
        public async Task<ActionResult> DeleteLookUp(int id)
        {
            var success = await lookUpService.DeleteLookUp(id);

            if (success)
                return Ok(id);
            else
                return NotFound();
        }
    }
}
