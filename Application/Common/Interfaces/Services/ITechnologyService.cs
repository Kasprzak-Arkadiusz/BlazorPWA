using Application.Commands.Technology;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface ITechnologyService
    {
        public Task<List<GetTechnologiesQuery>> GetAllAsync();

        public Task<int> AddAsync(CreateTechnology t);

        public Task UpdateAsync(UpdateTechnology t);

        public Task<bool> DeleteAsync(int id);
    }
}