using BitZenVeiculos.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace BitZenVeiculos.Domain.Contracts
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(Guid vehicleId);
        Task<bool> CreateVehicle(Vehicle vehicle);
        Task<bool> UpdateVehicle(Vehicle Vehicle);
        Task<bool> DeleteVehicle(Vehicle Vehicle);
        Task<bool> ExistsVehicle(Guid vehicleId);
        Task<bool> ExistsVehicleInFuelSupply(Guid vehicleId);
        Task<bool> Save();
    }
}
