using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BitZenVeiculos.Repository.Repositories
{
    public class FuelSupplyRepository : IFuelSupplyRepository
    {
        private readonly BitZenVeiculosContext _fuelSupplyContext;

        public FuelSupplyRepository(BitZenVeiculosContext fuelSupplyContext)
        {
            _fuelSupplyContext = fuelSupplyContext;
        }
        public async Task<bool> CreateFuelSupply(FuelSupply fuelSupply)
        {
            _fuelSupplyContext.Add(fuelSupply);
            return await Save();
        }

        public async Task<bool> UpdateFuelSupply(FuelSupply fuelSupply)
        {
            _fuelSupplyContext.Update(fuelSupply);
            return await Save();
        }

        public async Task<bool> DeleteFuelSupply(FuelSupply fuelSupply)
        {
            _fuelSupplyContext.Remove(fuelSupply);
            return await Save();
        }

        public async Task<bool> ExistsFuelSupply(Guid fuelSupplyId)
        {
            return await _fuelSupplyContext.FuelsSuplly.AsNoTracking().AnyAsync(fs => fs.Id == fuelSupplyId);

        }

        public async Task<FuelSupply> GetFuelSupply(Guid fuelSupplyId)
        {
            if (fuelSupplyId == Guid.Empty)
                throw new ArgumentNullException(nameof(fuelSupplyId));

            return await _fuelSupplyContext.FuelsSuplly.Where(fs => fs.Id == fuelSupplyId).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            return await _fuelSupplyContext.SaveChangesAsync() >= 0 ? true : false;
        }

       
    }
}
