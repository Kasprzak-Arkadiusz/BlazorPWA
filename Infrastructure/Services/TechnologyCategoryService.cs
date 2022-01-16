using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.TechnologyCategory;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Queries.TechnologyCategory;

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

        public async Task<GetTechnologyCategoryDetailQuery> GetByIdAsync(int id)
        {
            var category = await _technologyCategoryRepository.GetByIdAsync(id);
            return category;
        }

        public async Task AddAsync(CreateTechnologyCategory t)
        {
            if (t is null)
                throw new ArgumentNullException(nameof(t), "Created technology category can not be null");

            await _technologyCategoryRepository.AddAsync(t);
        }

        public async Task UpdateAsync(UpdateTechnologyCategory t)
        {
            if (t is null)
                throw new ArgumentNullException(nameof(t), "Updated technology category can not be null");

            await _technologyCategoryRepository.UpdateAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be lesser than 1");

            var result = await _technologyCategoryRepository.DeleteAsync(id);
            return result;
        }
    }
}