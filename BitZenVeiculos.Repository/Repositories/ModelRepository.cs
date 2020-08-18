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
    public class ModelRepository : IModelRepository
    {
        private readonly BitZenVeiculosContext _modelContext;

        public ModelRepository(BitZenVeiculosContext modelContext)
        {
            _modelContext = modelContext;
        }
        public async Task<bool> CreateModel(Model model)
        {
            _modelContext.Add(model);
            return await Save();
        }

        public async Task<bool> ExistsModel(Guid modelId)
        {
            return await _modelContext.Models.AsNoTracking().AnyAsync(m => m.Id == modelId);
        }

        public async Task<Model> GetModel(Guid modelId)
        {
            return await _modelContext.Models.Where(m => m.Id == modelId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Model>> GetModels()
        {
            return await _modelContext.Models.AsNoTracking().ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _modelContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
