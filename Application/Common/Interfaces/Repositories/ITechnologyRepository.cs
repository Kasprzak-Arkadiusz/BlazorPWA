using Application.Commands.Technology;
using Application.Queries.Technology;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITechnologyRepository
    {
        public Task<List<GetTechnologiesQuery>> GetAllAsync();

        public Task<GetTechnologyDetailsQuery> GetByIdAsync(int id);

        public Task<int> AddAsync(CreateTechnology t);

        public Task UpdateAsync(UpdateTechnology t);

        public Task<bool> DeleteAsync(int id);
    }
}