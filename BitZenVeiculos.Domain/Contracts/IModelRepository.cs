using BitZenVeiculos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitZenVeiculos.Domain.Contracts
{
    public interface IModelRepository
    {
        Task<bool> CreateModel(Model model);
        Task<bool> ExistsModel(Guid modelId);
        Task<Model> GetModel(Guid modelId);
        Task<IEnumerable<Model>> GetModels();
        Task<bool> Save();
    }
}
