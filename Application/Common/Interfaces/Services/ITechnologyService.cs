using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Technology;
using Application.Queries.Technology;

namespace Application.Common.Interfaces.Services
{
    public interface ITechnologyService
    {
        public Task<List<GetTechnologiesQuery>> GetAllAsync();

        public Task<GetTechnologyDetailsQuery> GetByIdAsync(int id);

        public Task AddAsync(CreateTechnology t);

        public Task UpdateAsync(UpdateTechnology t);

        public Task<bool> DeleteAsync(int id);
    }
}