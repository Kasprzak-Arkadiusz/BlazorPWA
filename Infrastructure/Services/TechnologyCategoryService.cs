using Application.Commands.TechnologyCategory;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries;
using Application.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TechnologyCategoryService : ITechnologyCategoryService
    {
        private readonly ITechnologyCategoryRepository _technologyCategoryRepository;

        public TechnologyCategoryService(ITechnologyCategoryRepository technologyCategoryRepository)
        {
            _technologyCategoryRepository = technologyCategoryRepository;
        }

        public async Task<List<GetTechnologyCategoriesQuery>> GetAllAsync()
        {
            var categories = await _technologyCategoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<int> AddAsync(CreateTechnologyCategory t)
        {
            TechnologyCategoryValidator.Validate(t);

            var id = await _technologyCategoryRepository.AddAsync(t);

            return id;
        }

        public async Task UpdateAsync(UpdateTechnologyCategory t)
        {
            TechnologyCategoryValidator.Validate(t);

            await _technologyCategoryRepository.UpdateAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            IdValidator.Validate(id);

            var result = await _technologyCategoryRepository.DeleteAsync(id);
            return result;
        }
    }
}