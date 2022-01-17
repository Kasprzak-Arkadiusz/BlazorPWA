using Application.Queries.TechnologyCategory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.HttpRepository.Categories
{
    public class TechnologyCategoryHttpRepository : BaseHttpRepository, ITechnologyCategoryHttpRepository
    {
        public TechnologyCategoryHttpRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<GetTechnologyCategoriesQuery>> GetAllCategoriesAsync()
        {
            var response = await HttpClient.GetAsync("TechnologyCategories");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var technologies = JsonSerializer.Deserialize<List<GetTechnologyCategoriesQuery>>(content, Options);

            return technologies;
        }
    }
}