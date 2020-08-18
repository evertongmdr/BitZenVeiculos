using BitZenVeiculos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitZenVeiculos.Domain.Contracts
{
    public interface IMakeRepository
    {
        Task<bool> CreateMake(Make make);
        Task<bool> ExistsMake(Guid makeId);
        Task<Make> GetMake(Guid makeId);
        Task<IEnumerable<Make>> GetMakes();
        Task<bool> Save();
    }
}
