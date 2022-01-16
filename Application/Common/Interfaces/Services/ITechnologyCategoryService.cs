using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.TechnologyCategory;
using Application.Queries.TechnologyCategory;

namespace Application.Common.Interfaces.Services
{
    public interface ITechnologyCategoryService
    {
        public Task<List<GetTechnologyCategoriesQuery>> GetAllAsync();

        public Task<GetTechnologyCategoryDetailQuery> GetByIdAsync(int id);

        public Task AddAsync(CreateTechnologyCategory t);

        public Task UpdateAsync(UpdateTechnologyCategory t);

        public Task<bool> DeleteAsync(int id);
    }
}