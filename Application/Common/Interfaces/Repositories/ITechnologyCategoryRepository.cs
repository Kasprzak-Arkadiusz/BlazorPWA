using Application.Commands.TechnologyCategory;
using Application.Queries.TechnologyCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITechnologyCategoryRepository
    {
        public Task<List<GetTechnologyCategoriesQuery>> GetAllAsync();

        public Task<int> AddAsync(CreateTechnologyCategory t);

        public Task UpdateAsync(UpdateTechnologyCategory t);

        public Task<bool> DeleteAsync(int id);
    }
}