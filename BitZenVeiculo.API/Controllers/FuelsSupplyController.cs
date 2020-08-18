using AutoMapper;
using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static BitZenVeiculos.Domain.DTOs.FuelSupplyDTO;

namespace BitZenVeiculos.API.Controllers
{
    [Route("api/fuels-supply")]
    [ApiController]
    [Authorize]
    public class FuelsSupplyController : ControllerBase
    {
        private readonly IFuelSupplyRepository _fuelSupplyRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FuelsSupplyController(
            IFuelSupplyRepository fuelSupplyRepository,
            IVehicleRepository vehicleRepository, 
            IUserRepository userRepository, 
            IMapper mapper)
        {
            

            _fuelSupplyRepository=fuelSupplyRepository;
            _vehicleRepository = vehicleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateFuelSupply([FromBody] FuelSupply fuelSupply)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statusCode = await ValidateFuelSupply(fuelSupply);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode, ModelState);

            if (!await _fuelSupplyRepository.CreateFuelSupply(fuelSupply))
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao salvar o abasteciemnto");
                return StatusCode(500, ModelState);
            }

            var fuelSupplyDTO = _mapper.Map<FuelSupplyResponseDTO>(fuelSupply);
            return CreatedAtRoute("GetFuelSupply", new { fuelSupplyId = fuelSupplyDTO.Id }, fuelSupplyDTO);
        }

        [HttpPut("{fuelSupplyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateFuelSupply(Guid fuelSupplyId, [FromBody] FuelSupplyResponseDTO fuelSupply)
        {
            var fuelSupplyEntity = await _fuelSupplyRepository.GetFuelSupply(fuelSupplyId);

            if (fuelSupplyEntity == null)
                return NotFound();

            _mapper.Map(fuelSupply, fuelSupplyEntity);

            if (!TryValidateModel(fuelSupplyEntity))
                return ValidationProblem(ModelState);

            var statusCode = await ValidateFuelSupply(fuelSupplyEntity);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode, ModelState);

            if (!await _fuelSupplyRepository.Save())
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao atualizar o abasteciemnto");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{fuelSupplyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteFuelSupply(Guid fuelSupplyId)
        {
            var fuelSupplyEntity = await _fuelSupplyRepository.GetFuelSupply(fuelSupplyId);

            if (fuelSupplyEntity == null)
                return NotFound();

            if (!await _fuelSupplyRepository.DeleteFuelSupply(fuelSupplyEntity))
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao deletar o abastecimento com");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("{fuelSupplyId}", Name = "GetFuelSupply")]
        [ProducesResponseType(200, Type = typeof(FuelSupplyResponseDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFuelSupply(Guid fuelSupplyId)
        {
            var fuelSupplyEntity = await _fuelSupplyRepository.GetFuelSupply(fuelSupplyId);

            if (fuelSupplyEntity == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fuelSupply = _mapper.Map<FuelSupplyResponseDTO>(fuelSupplyEntity);

            return Ok(fuelSupply);
        }

        private async Task<StatusCodeResult> ValidateFuelSupply(FuelSupply fuelSupply)
        {
            Guid responsibleId = new Guid(fuelSupply.ResponsibleUserId.ToString());
            Guid vehicleId = new Guid(fuelSupply.VehicleId.ToString());

            bool existsError = false;

            if (!Enum.IsDefined(typeof(FuelType), fuelSupply.FuelType))
            {
                ModelState.AddModelError("", "Este tipo de combustível não existe, para mais informação consute o suporte");
                existsError = true;
            }

            if (!await _userRepository.UserExists(responsibleId))
            {
                ModelState.AddModelError("", "O usuário informado não existe");
                existsError = true;
            }

            if (!await _vehicleRepository.ExistsVehicle(vehicleId))
            {
                ModelState.AddModelError("", "O veículo informado não existe");
                existsError = true;
            }

            return existsError ? StatusCode(404) : null;
        }
    }
}
