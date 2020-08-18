using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitZenVeiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MakesController : ControllerBase
    {
        private readonly IMakeRepository _makeRepository;

        public MakesController(IMakeRepository makeRepository)
        {
            _makeRepository = makeRepository;
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateMake([FromBody] Make make)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _makeRepository.CreateMake(make))
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao salvar a marca {make.Description}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetMake", new { makeId = make.Id }, make);
        }

        [HttpGet("{makeId}", Name = "GetMake")]
        [ProducesResponseType(200, Type = typeof(Make))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetMake(Guid makeId)
        {
            var make = await _makeRepository.GetMake(makeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(make);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Make>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            var makes = await _makeRepository.GetMakes();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(makes);
        }
    }
}
