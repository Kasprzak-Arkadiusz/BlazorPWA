using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.Technology;
using Application.Queries.Technology;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITechnologyRepository
    {
        Task<List<GetTechnologiesQuery>> GetAllAsync();

        Task<GetTechnologyDetailsQuery> GetByIdAsync(int id);

        Task AddAsync(CreateTechnology t);

        Task UpdateAsync(UpdateTechnology t);

        Task<bool> DeleteAsync(int id);
    }
}