using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BitZenVeiculos.Repository.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly BitZenVeiculosContext _vehicleContext;
        public VehicleRepository(BitZenVeiculosContext vehicleContext)
        {
            _vehicleContext = vehicleContext;
        }
        public async Task<bool> CreateVehicle(Vehicle vehicle)
        {
            _vehicleContext.Add(vehicle);
            return await Save();
        }

        public async Task<bool> DeleteVehicle(Vehicle Vehicle)
        {
            _vehicleContext.Remove(Vehicle);
            return await Save();
        }

        public async Task<Vehicle> GetVehicle(Guid vehicleId)
        {
            if (vehicleId == Guid.Empty)
                throw new ArgumentNullException(nameof(vehicleId));

            return await _vehicleContext.Vehicles.Where(v => v.Id == vehicleId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateVehicle(Vehicle Vehicle)
        {
            _vehicleContext.Update(Vehicle);
            return await Save();
        }
        public async Task<bool> ExistsVehicleInFuelSupply(Guid vehicleId)
        {
            return await _vehicleContext.FuelsSuplly.AnyAsync(fs => fs.VehicleId == vehicleId);
        }

        public async Task<bool> Save()
        {
            return await _vehicleContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<bool> ExistsVehicle(Guid vehicleId)
        {
            return await _vehicleContext.Vehicles.AsNoTracking().AnyAsync(v => v.Id == vehicleId);
        }
    }
}
