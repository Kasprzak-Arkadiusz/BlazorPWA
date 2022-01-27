using Application.Commands.Technology;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.Technology;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyService(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task<List<GetTechnologiesQuery>> GetAllAsync()
        {
            var technologies = await _technologyRepository.GetAllAsync();
            return technologies;
        }

        public async Task<GetTechnologyDetailsQuery> GetByIdAsync(int id)
        {
            var technology = await _technologyRepository.GetByIdAsync(id);
            return technology;
        }

        public async Task<int> AddAsync(CreateTechnology t)
        {
            if (t is null)
                throw new ArgumentNullException(nameof(t), "Created technology can not be null");

            var id = await _technologyRepository.AddAsync(t);

            return id;
        }

        public async Task UpdateAsync(UpdateTechnology t)
        {
            if (t is null)
                throw new ArgumentNullException(nameof(t), "Updated technology can not be null");

            await _technologyRepository.UpdateAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be lesser than 1");

            var result = await _technologyRepository.DeleteAsync(id);
            return result;
        }
    }
}