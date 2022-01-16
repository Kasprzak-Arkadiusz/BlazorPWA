using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Commands.TechnologyCategory;
using Application.Queries.TechnologyCategory;

namespace Application.Common.Interfaces.Repositories
{
    public interface ITechnologyCategoryRepository
    {
        Task<List<GetTechnologyCategoriesQuery>> GetAllAsync();
        Task<GetTechnologyCategoryDetailQuery> GetByIdAsync(int id);

        Task AddAsync(CreateTechnologyCategory t);

        Task UpdateAsync(UpdateTechnologyCategory t);

        Task<bool> DeleteAsync(int id);
    }
}