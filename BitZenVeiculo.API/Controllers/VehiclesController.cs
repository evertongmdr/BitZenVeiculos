using AutoMapper;
using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static BitZenVeiculos.Domain.DTOs.VehicleDTO;

namespace BitZenVeiculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMakeRepository _makeRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public VehiclesController(
            IVehicleRepository vehicleRepository,
            IMakeRepository makeRepository,
            IModelRepository modelRepository,
            IUserRepository userRepository,
            IMapper mapper
            )
        {
            _vehicleRepository = vehicleRepository;
            _makeRepository = makeRepository;
            _userRepository = userRepository;
            _modelRepository = modelRepository;

            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateVehicle([FromBody] Vehicle vehicle)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

          var statusCode = await ValidateVechile(vehicle);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode, ModelState);

            if (!await _vehicleRepository.CreateVehicle(vehicle))
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao salvar veículo com a placa {vehicle.LicensePlate}");
                return StatusCode(500, ModelState);
            }

            var vhicleDTO = _mapper.Map<VehicleResponseDTO>(vehicle);
            return CreatedAtRoute("GetVehicle", new { vehicleId = vhicleDTO.Id }, vhicleDTO);
        }

        [HttpPut("{vehicleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateVehicle(Guid vehicleId, [FromBody] VehicleResponseDTO vehicle)
        {
            var vechileEntity = await _vehicleRepository.GetVehicle(vehicleId);

            if (vechileEntity == null)
                return NotFound();

            _mapper.Map(vehicle, vechileEntity);

            if (!TryValidateModel(vechileEntity))
                return ValidationProblem(ModelState);

            var statusCode = await ValidateVechile(vechileEntity);

            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode, ModelState);

            if (!await _vehicleRepository.Save())
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao atualizar veículo com a placa {vehicle.LicensePlate}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{vechileId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteVehicle(Guid vechileId)
        {
            var vehicleEntity = await _vehicleRepository.GetVehicle(vechileId);

            if (vehicleEntity == null)
                return NotFound();

            if (await _vehicleRepository.ExistsVehicleInFuelSupply(vechileId))
            {
                ModelState.AddModelError("", $"O veículo com a placa {vehicleEntity.LicensePlate} não pode " +
                    $"ser deletado, porque já faz parte de algu(ns ou m) abastecimento(s)");
                return StatusCode(409, ModelState);
            }

            if (!await _vehicleRepository.DeleteVehicle(vehicleEntity))
            {
                ModelState.AddModelError("", $"Ocorreu um erro ao deletar o veículo com a placa {vehicleEntity.LicensePlate}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("{vehicleId}", Name = "GetVehicle")]
        [ProducesResponseType(200, Type = typeof(VehicleResponseDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetVehicle(Guid vehicleId)
        {
            var vehicleEntity = await _vehicleRepository.GetVehicle(vehicleId);

            if (vehicleEntity == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = _mapper.Map<VehicleResponseDTO>(vehicleEntity);

            return Ok(vehicle);
        }

        private async Task<StatusCodeResult> ValidateVechile(Vehicle vehicle)
        {
            bool existsError = false;

            if (!Enum.IsDefined(typeof(FuelType), vehicle.FuelType))
            {
                ModelState.AddModelError("", "Este tipo de combustível não existe, para mais informação consute o suporte");
                existsError = true;
            }

            if (!Enum.IsDefined(typeof(VehicleType), vehicle.VehicleType))
            {
                ModelState.AddModelError("", "Este tipo de veículo não existe, para mais informação consulte o suporte");
                existsError = true;
            }

            if (!await _makeRepository.ExistsMake(vehicle.MakeId))
            {
                ModelState.AddModelError("", "Esta marca não existe");
                existsError = true;
            }

            if (!await _modelRepository.ExistsModel(vehicle.ModelId))
            {
                ModelState.AddModelError("", "Este modelo não existe");
                existsError = true;
            }

            if (!await _userRepository.UserExists(vehicle.ResponsibleUserId))
            {
                ModelState.AddModelError("", "Este usuário não existe");
                existsError = true;
            }

            return existsError ? StatusCode(404) : null;
        }
    }
}
