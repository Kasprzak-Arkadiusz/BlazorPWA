using Application.Commands.TechnologyCategory;
using Application.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Categories
{
    public interface ITechnologyCategoriesHttpRepository
    {
        Task<List<GetTechnologyCategoriesQuery>> GetAllCategoriesAsync();

        Task<int> CreateTechnologyCategoryAsync(CreateTechnologyCategory categoryToAdd);

        Task<bool> UpdateTechnologyCategoryAsync(UpdateTechnologyCategory categoryToUpdate);

        Task<bool> DeleteTechnologyCategoryAsync(int id);
    }
}