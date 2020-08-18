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
    public class ModelsController : ControllerBase
    {
        private readonly IModelRepository _modelRepository;

        public ModelsController(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateModel([FromBody] Model model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _modelRepository.CreateModel(model))
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao salvar o modelo {model.Description}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetModel", new { modelId = model.Id }, model);
        }

        [HttpGet("{modelId}", Name = "GetModel")]
        [ProducesResponseType(200, Type = typeof(Model))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetModel(Guid modelId)
        {
            var model = await _modelRepository.GetModel(modelId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Model>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        {
            var models = await _modelRepository.GetModels();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(models);
        }
    }
}
