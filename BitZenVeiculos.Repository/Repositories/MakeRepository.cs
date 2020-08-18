using BitZenVeiculos.Domain.Contracts;
using BitZenVeiculos.Domain.Entities;
using BitZenVeiculos.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitZenVeiculos.Repository.Repositories
{
    public class MakeRepository : IMakeRepository
    {
        private readonly BitZenVeiculosContext _makeContext;

        public MakeRepository(BitZenVeiculosContext makeContext)
        {
            _makeContext = makeContext;
        }
        public async Task<bool> CreateMake(Make make)
        {
            _makeContext.Add(make);
            return await Save();
        }

        public async Task<bool> ExistsMake(Guid makeId)
        {
            return await _makeContext.Makes.AsNoTracking().AnyAsync(m => m.Id == makeId);
        }

        public async Task<Make> GetMake(Guid makeId)
        {
            return await _makeContext.Makes.Where(m => m.Id == makeId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Make>> GetMakes()
        {
            return await _makeContext.Makes.ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _makeContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
