using Application.Commands.Technology;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries;
using Application.Validators;
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

        public async Task<int> AddAsync(CreateTechnology t)
        {
            TechnologyValidator.Validate(t);

            var id = await _technologyRepository.AddAsync(t);
            return id;
        }

        public async Task UpdateAsync(UpdateTechnology t)
        {
            TechnologyValidator.Validate(t);

            await _technologyRepository.UpdateAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            IdValidator.Validate(id);

            var result = await _technologyRepository.DeleteAsync(id);
            return result;
        }
    }
}