using BitZenVeiculos.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace BitZenVeiculos.Domain.Contracts
{
    public interface IFuelSupplyRepository
    {
        Task<bool> CreateFuelSupply(FuelSupply fuelSupply);
        Task<bool> UpdateFuelSupply(FuelSupply fuelSupply);
        Task<bool> DeleteFuelSupply(FuelSupply fuelSupply);
        Task<FuelSupply> GetFuelSupply(Guid fuelSupplyId);
        Task<bool> ExistsFuelSupply(Guid fuelSupplyId);
        Task<bool> Save();
    }
}
