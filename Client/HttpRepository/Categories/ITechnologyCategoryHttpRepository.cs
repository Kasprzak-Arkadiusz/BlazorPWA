using Application.Queries.TechnologyCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.HttpRepository.Categories
{
    public interface ITechnologyCategoryHttpRepository
    {
        Task<List<GetTechnologyCategoriesQuery>> GetAllCategoriesAsync();
    }
}